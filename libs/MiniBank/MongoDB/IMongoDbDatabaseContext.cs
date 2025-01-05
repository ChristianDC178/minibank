using MongoDB.Driver;

namespace MiniBank.MongoDB;

public interface IMongoDbDatabaseContext<T>
{
    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }

    public IMongoCollection<T> Collection { get; }
}

public class MongoEntityDbContext<T>
(
    IMongoClientWrapper mongoClient
)
: IMongoDbDatabaseContext<T>
{

    public string DatabaseName { get; set; }
    public string CollectionName { get; set; }

    public IMongoCollection<T> Collection
    {
        get => mongoClient.GetCollection<T>(DatabaseName, CollectionName);
    }

}
