using MyChatApp.Core.Abstract.Base.Repositories;
using MyChatApp.Domain.Concrete.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChatApp.Core.Abstract.Repositories
{
    public interface IMessageRepository : ICrudRepository<Message>
    {
    }
}
