using MongoDB.Driver;
using MyChatApp.Core.Abstract.Base.MongoDB;
using MyChatApp.Core.Abstract.Repositories;
using MyChatApp.Core.Concrete.Base.Repositories;
using MyChatApp.Domain.Concrete.Entities;

namespace MyChatApp.Core.Concrete.Repositories
{
    public class MessageRepository : CrudRepository<Message>, IMessageRepository
    {
        public MessageRepository(IMongoStoreDatabaseSettings settings, IMongoClient mongoClient) : base(settings, mongoClient)
        {
        }
    }
}
