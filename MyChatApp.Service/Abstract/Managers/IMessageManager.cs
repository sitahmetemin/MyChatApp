using MyChatApp.Domain.Concrete.Entities;
using MyChatApp.Service.Abstract.Base.Managers;

namespace MyChatApp.Service.Abstract.Managers
{
    public interface IMessageManager : ICrudManager<Message>
    {
    }
}
