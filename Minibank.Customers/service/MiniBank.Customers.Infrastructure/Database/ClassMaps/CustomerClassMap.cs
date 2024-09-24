using MiniBank.CustomersSrv.Domain.Entities;
using MongoDB.Bson.Serialization;
using MiniBank.MongoDB;

namespace MiniBank.CustomersSrv.Infrastructure.Database.ClassMaps;

public class CustomerClassMap : BsonClassMapBuilder<Customer>
{
    public override void RegisterClassMap()
    {
        map.MapIdField(c => c.EntityId).SetElementName("entity_id");
        map.MapProperty(c => c.FirstName).SetElementName("first_name").SetIsRequired(true);
        map.MapProperty(c => c.LastName).SetElementName("last_name");
        map.MapProperty(c => c.Document).SetElementName("document");
    }
}
