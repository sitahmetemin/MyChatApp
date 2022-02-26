using MongoDB.Driver;
using MyChatApp.Core.Abstract.Base.MongoDB;
using MyChatApp.Core.Abstract.Repositories;
using MyChatApp.Core.Concrete.Base.Repositories;
using MyChatApp.Domain.Concrete.Entities;

namespace MyChatApp.Core.Concrete.Repositories
{
    public class ActionLogRepository : CrudRepository<ActionLog>, IActionLogRepository
    {
        public ActionLogRepository(IMongoStoreDatabaseSettings settings, IMongoClient mongoClient) : base(settings, mongoClient)
        {
        }
    }
}
