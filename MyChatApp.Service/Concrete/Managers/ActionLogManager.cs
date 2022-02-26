using MyChatApp.Core.Abstract.Base.Repositories;
using MyChatApp.Core.Abstract.Repositories;
using MyChatApp.Domain.Concrete.Entities;
using MyChatApp.Service.Abstract.Managers;
using MyChatApp.Service.Concrete.Base.Managers;

namespace MyChatApp.Service.Concrete.Managers
{
    public class ActionLogManager : CrudManager<ActionLog>, IActionLogManager
    {
        public ActionLogManager(IActionLogRepository actionLogRepository) : base(actionLogRepository)
        {
        }
    }
}
