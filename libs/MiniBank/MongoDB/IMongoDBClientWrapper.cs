using MongoDB.Driver;

namespace MiniBank.MongoDB;

public interface IMongoClientWrapper
{
    public MongoClient Client { get; }
    public IMongoCollection<T> GetCollection<T>(string database, string collection);
}
