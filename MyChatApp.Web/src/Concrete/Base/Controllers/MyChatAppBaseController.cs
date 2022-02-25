using Microsoft.AspNetCore.Mvc;

namespace MyChatApp.Web.src.Concrete.Base.Controllers
{
    public class MyChatAppBaseController<TController, TEntity> : Controller
    {
        protected readonly ILogger<TController> _logger;
        protected readonly string _viewName;

        public MyChatAppBaseController(ILogger<TController> logger, string viewName)
        {
            _logger = logger;
            _viewName = viewName;
        }

    }
}
