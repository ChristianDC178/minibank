using MongoDB.Bson.Serialization;
using System.Net;

namespace MiniBank.MongoDB;

public abstract class BsonClassMapBuilder<T>
{
    protected BsonClassMap<T> map = new BsonClassMap<T>();

    public BsonClassMap<T> BsonMap 
    { 
        get => map;
    }

    //public abstract BsonClassMap<T> RegisterClassMap();
    public abstract void RegisterClassMap();
}
