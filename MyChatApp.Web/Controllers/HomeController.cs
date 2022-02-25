using Microsoft.AspNetCore.Mvc;
using MyChatApp.Domain.Concrete.Entities;
using MyChatApp.Service.Abstract.Managers;
using MyChatApp.Web.Models;
using MyChatApp.Web.src.Concrete.Base.Controllers;
using System.Diagnostics;

namespace MyChatApp.Web.Controllers
{
    public class HomeController : MyChatAppBaseController<HomeController, Message>
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMessageManager _messageManager;

        public HomeController(ILogger<HomeController> logger,
            IMessageManager messageManager)
            : base(logger, "Home")
        {
            _logger = logger;
            _messageManager = messageManager;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [HttpPost]
        public async Task<IList<Message>> GetTalkHistory(string channelName)
        {
            try
            {
                var result = await _messageManager.GetListByFilterAsync(q => q.Channel == channelName);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}