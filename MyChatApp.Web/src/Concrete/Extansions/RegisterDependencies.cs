using MyChatApp.Core.Abstract.Base.Repositories;
using MyChatApp.Core.Abstract.Repositories;
using MyChatApp.Core.Concrete.Base.Repositories;
using MyChatApp.Core.Concrete.Repositories;
using MyChatApp.Service.Abstract.Base.Managers;
using MyChatApp.Service.Abstract.Managers;
using MyChatApp.Service.Concrete.Base.Managers;
using MyChatApp.Service.Concrete.Managers;

namespace MyChatApp.Web.src.Concrete.Extansions
{
    public static class RegisterDependencies
    {
        public static void RegisterRepository(this IServiceCollection services)
        {
            services.AddTransient(typeof(ICrudRepository<>), typeof(CrudRepository<>));
            services.AddTransient<IMessageRepository, MessageRepository>();
            services.AddTransient<IActionLogRepository, ActionLogRepository>();
        }

        public static void RegisterManager(this IServiceCollection services)
        {
            services.AddTransient<IActionLogManager, ActionLogManager>();
            services.AddScoped(typeof(ICrudManager<>), typeof(CrudManager<>));
            services.AddScoped<IMessageManager, MessageManager>();
        }
    }
}
