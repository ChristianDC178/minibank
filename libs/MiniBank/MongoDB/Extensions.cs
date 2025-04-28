using MiniBank.Domain;
using MongoDB.Bson.Serialization;

namespace MiniBank.MongoDB.Extensions;

public static class Extensions
{

    public static BsonClassMap<T> ConfigAuditableProperties<T>(this BsonClassMap<T> bsonClassMap) 
    {
        if (bsonClassMap.ClassType.BaseType == typeof(AuditableEntity))
        {
            bsonClassMap.MapProperty("CreatedDate").SetElementName("created_date");
            bsonClassMap.MapProperty("UpdatedDate").SetElementName("updated_date");
        }
        return bsonClassMap;
    }

}
