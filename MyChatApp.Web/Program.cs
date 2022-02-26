using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyChatApp.Core.Abstract.Base.MongoDB;
using MyChatApp.Core.Concrete.MongoDB;
using MyChatApp.Web.src.Concrete.Extansions;
using MyChatApp.Web.src.Concrete.Extansions.Middlewares;
using MyChatApp.Web.src.Concrete.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

#region MongoDBConf
builder.Services.Configure<MongoStoreDatabaseSettings>(builder.Configuration.GetSection(nameof(MongoStoreDatabaseSettings)));

builder.Services.AddSingleton<IMongoStoreDatabaseSettings>(sp => sp.GetRequiredService<IOptions<MongoStoreDatabaseSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s => new MongoClient(builder.Configuration.GetValue<string>("MongoStoreDatabaseSettings:ConnectionString")));
#endregion

#region RegisterDependencies
builder.Services.RegisterRepository();
builder.Services.RegisterManager();
builder.Services.AddSignalR();
#endregion

var app = builder.Build();

app.MapHub<ChatHub>("/chatLobby");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//app.UseAuthorization();

app.UseMiddleware<RequestResponseLoggingMiddleware>();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
