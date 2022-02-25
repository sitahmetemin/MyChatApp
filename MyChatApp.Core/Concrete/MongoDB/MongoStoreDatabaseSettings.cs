using MyChatApp.Core.Abstract.Base.MongoDB;

namespace MyChatApp.Core.Concrete.MongoDB
{
    public class MongoStoreDatabaseSettings : IMongoStoreDatabaseSettings
    {
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
        public string CollectionName { get; set; } = String.Empty;
    }
}
