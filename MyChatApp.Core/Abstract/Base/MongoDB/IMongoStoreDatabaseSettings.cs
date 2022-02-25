namespace MyChatApp.Core.Abstract.Base.MongoDB
{
    public interface IMongoStoreDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string CollectionName { get; set; }
    }
}
