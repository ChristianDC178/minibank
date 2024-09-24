namespace MiniBank.Cache;

public interface IRedisClientWrapper
{
    public StackExchange.Redis.IDatabase Database { get; }
}
