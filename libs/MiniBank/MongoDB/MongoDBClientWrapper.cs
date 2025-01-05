using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System.Security.Cryptography.X509Certificates;

namespace MiniBank.MongoDB;

public class MongoClientWrapper : IMongoClientWrapper
{

    private readonly MongoClient _client;
    private readonly IConfiguration _config;
    

    public MongoClientWrapper(IConfiguration config)
    {
        _config = config;
        var url = config["Mongo:url"];
        MongoUrl mongoUrl = new MongoUrl("mongodb://localhost:27017");
        _client = new MongoClient(mongoUrl);
    }

    public MongoClient Client
    {
        get => _client;
    }

    public IMongoCollection<T> GetCollection<T>(string database, string collection)
    { 
        return Client.GetDatabase(database).GetCollection<T>(collection);
    }

}
