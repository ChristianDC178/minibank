using MiniBank.AccountsAndTransactions.Domain.Entities;
using MongoDB.Bson.Serialization;
using MiniBank.MongoDB;
using MiniBank.MongoDB.Extensions;
using System.Net;

namespace MiniBank.CustomersSrv.Infrastructure.Database.ClassMaps;

public class AddressClassMap : BsonClassMapBuilder<DepositAccount>
{
    public override void RegisterClassMap()
    {
        map.ConfigAuditableProperties<DepositAccount>();
    }
}

