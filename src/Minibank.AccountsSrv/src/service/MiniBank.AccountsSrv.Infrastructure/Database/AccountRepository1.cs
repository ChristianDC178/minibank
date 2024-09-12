using MiniBank.AccountsSrv.Domain.Entities;
using MiniBank.AccountsSrv.Domain.Repositories;
using MongoDB.Driver;

namespace MiniBank.AccountsSrv.Infrastructure.Database;

public class AccountRepository : IAccountRepository
{

    private readonly IMongoClient _mongoClient1;

    public AccountRepository(IMongoClient mongoClient)
    {
        _mongoClient1 = mongoClient;
    }

    public async Task<bool> SaveCustomer(Account customer, CancellationToken cancellationToken)
    {
        MongoUrl mongoUrl = new MongoUrl("mongodb://localhost:27017");
        MongoClient client = new MongoClient(mongoUrl);
        var database = client.GetDatabase("customers_db");
        var collection = database.GetCollection<Account>("customers");

        InsertOneOptions insertOneOptions = new InsertOneOptions();
        insertOneOptions.Comment = "Inserting a new person";

        await collection.InsertOneAsync(customer, insertOneOptions, cancellationToken);

        return true;
    }

    public async Task<bool> UpdateCustomer(Domain.Entities.Account customer, CancellationToken cancellationToken)
    {

        MongoUrl mongoUrl = new MongoUrl("mongodb://localhost:27017");
        MongoClient client = new MongoClient(mongoUrl);
        var database = client.GetDatabase("customers_db");
        var collection = database.GetCollection<Account>("customers");

        InsertOneOptions insertOneOptions = new InsertOneOptions();
        insertOneOptions.Comment = "Inserting a new person";

        //Arreglar
        //await collection.UpdateOneAsync(customer, insertOneOptions, cancellationToken);

        return true;
    }

}