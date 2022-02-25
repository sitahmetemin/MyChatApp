using MyChatApp.Core.Abstract.Base.Repositories;
using MyChatApp.Domain.Concrete.Entities;
using MyChatApp.Service.Abstract.Managers;
using MyChatApp.Service.Concrete.Base.Managers;

namespace MyChatApp.Service.Concrete.Managers
{
    public class MessageManager : CrudManager<Message>, IMessageManager
    {
        public MessageManager(ICrudRepository<Message> crudRepository) : base(crudRepository)
        {
        }
    }
}
