using MiniBank.AccountsAndTransactions.Domain.Entities;
using MongoDB.Bson.Serialization;
using MiniBank.MongoDB;
using System.Net;

namespace MiniBank.CustomersSrv.Infrastructure.Database.ClassMaps;

public class AddressClassMap : BsonClassMapBuilder<DepositAccount>
{
    public override void RegisterClassMap()
    {
        map.MapProperty(c => c.City).SetElementName("city");
        map.MapProperty(c => c.State).SetElementName("state");
        map.MapProperty(c => c.StreetName).SetElementName("street_name");
        map.MapProperty(c => c.StreetNumber).SetElementName("street_number");
        map.MapProperty(c => c.ZipCode).SetElementName("zip_code");
    }
}

