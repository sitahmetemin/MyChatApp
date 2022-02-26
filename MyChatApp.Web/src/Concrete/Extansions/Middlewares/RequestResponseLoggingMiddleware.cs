using Microsoft.IO;
using MyChatApp.Domain.Concrete.Entities;
using MyChatApp.Service.Abstract.Managers;
using System.Diagnostics;

namespace MyChatApp.Web.src.Concrete.Extansions.Middlewares
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IActionLogManager _actionLogManager;
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        const string correlationKey = "X-TraceId";

        public RequestResponseLoggingMiddleware(RequestDelegate next, 
            IActionLogManager actionLogManager)
        {
            _next = next;
            _actionLogManager = actionLogManager;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await LogRequest(context);
            await _next.Invoke(context);
            await LogResponse(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            if (!context.Request.Headers.Any(q => q.Key == correlationKey))
                context.Request.Headers.Add(correlationKey, Guid.NewGuid().ToString());
            await context.Request.Body.CopyToAsync(requestStream);

            string header = $@"[Schema:{context.Request.Scheme}] [Host: {context.Request.Host}] [Path: {context.Request.Path}] [QueryString: {context.Request.QueryString}]";


            ActionLog actionLog = new()
            {
                ContextId = context.Request.Headers.FirstOrDefault(q => q.Key == correlationKey).Value,
                Header = header,
                Body = ReadStreamInChunks(requestStream)
            };
            await _actionLogManager.AddAsync(actionLog);
            context.Request.Body.Position = 0;
        }

        private async Task LogResponse(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var body = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            string headerString = $"[Schema:{context.Request.Scheme} ] [Host: {context.Request.Host}] [Path: {context.Request.Path}] [QueryString: {context.Request.QueryString}]";

            ActionLog actionLog = new()
            {
                ContextId = context.Request.Headers.FirstOrDefault(q => q.Key == correlationKey).Value,
                Header = headerString,
                Body = body
            };
            await _actionLogManager.AddAsync(actionLog);

            await responseBody.CopyToAsync(originalBodyStream);
        }

        private string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
        }
    }
}
