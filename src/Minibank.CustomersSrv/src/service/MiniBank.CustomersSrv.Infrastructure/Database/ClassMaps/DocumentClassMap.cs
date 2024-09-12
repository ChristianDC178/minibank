using MiniBank.CustomersSrv.Domain.Entities;
using MongoDB.Bson.Serialization;
using MiniBank.MongoDB;

namespace MiniBank.CustomersSrv.Infrastructure.Database.ClassMaps;

public class DocumentClassMap : BsonClassMapBuilder<Document>
{
    public override void RegisterClassMap()
    {
        map.MapIdField(d => d.DocumentId).SetElementName("document_id");
        map.MapProperty(d => d.Type).SetElementName("type").SetIsRequired(true);
    }
}