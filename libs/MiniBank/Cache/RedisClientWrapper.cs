using StackExchange.Redis;

namespace MiniBank.Cache;

public class RedisClientWrapper : IRedisClientWrapper
{

    ConnectionMultiplexer _redisClient;
    IDatabase _database;

    public RedisClientWrapper()
    {
        _redisClient = ConnectionMultiplexer.Connect("localhost");
        _database = _redisClient.GetDatabase(1);
    }

    public StackExchange.Redis.IDatabase Database
    {
        get => _database;
    }

}
