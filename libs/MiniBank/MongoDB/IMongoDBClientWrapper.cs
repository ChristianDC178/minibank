using MongoDB.Driver;

namespace MiniBank.MongoDB;

public interface IMongoClientWrapper
{
    public MongoClient Client { get; }
}
