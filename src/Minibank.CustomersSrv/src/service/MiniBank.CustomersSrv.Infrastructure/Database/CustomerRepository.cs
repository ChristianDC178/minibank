using MiniBank.CustomersSrv.Domain.Entities;
using MiniBank.CustomersSrv.Domain.Repositories;
using MiniBank.MongoDB;
using MongoDB.Driver;

namespace MiniBank.CustomersSrv.Infrastructure.Database;

public class CustomerRepository
(
   IMongoClientWrapper mongoClient
)
: ICustomerRepository
{

    public async Task<bool> Save(Customer customer, CancellationToken cancellationToken)
    {
        try
        {

            var database = mongoClient.Client.GetDatabase("customers-srv");
            var collection = database.GetCollection<Customer>("customers");

            InsertOneOptions insertOneOptions = new InsertOneOptions();
            insertOneOptions.Comment = "Inserting a new person";

            await collection.InsertOneAsync(customer, insertOneOptions, cancellationToken);

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Customer> GetById(Guid customerId, CancellationToken cancellationToken)
    {

        var database = mongoClient.Client.GetDatabase("customers-srv");
        var collection = database.GetCollection<Customer>("customers");

        var filter = Builders<Customer>.Filter.Eq(c => c.EntityId, customerId);

        var customer = await collection.Find(filter).FirstOrDefaultAsync();

        return customer;
    }


    public async Task<bool> Update(Customer customer, CancellationToken cancellationToken)
    {
        var database = mongoClient.Client.GetDatabase("customers-srv");
        var collection = database.GetCollection<Customer>("customers");

        var filter = Builders<Customer>.Filter.Eq(c => c.EntityId, customer.EntityId);

        var update = Builders<Customer>.Update
            .Set(c => c.FirstName, customer.FirstName)
            .Set(c => c.LastName, customer.LastName)
            .Set(c => c.Document, customer.Document);

        var updateResult = await collection.UpdateOneAsync(filter, update);

        return true;

    }

    public async Task<bool> Replace(Customer customer, CancellationToken cancellationToken)
    {
        var database = mongoClient.Client.GetDatabase("customers-srv");
        var collection = database.GetCollection<Customer>("customers");

        var replacementResult = await collection.ReplaceOneAsync<Customer>((c) =>
                                c.EntityId == customer.EntityId, customer, cancellationToken: cancellationToken);

        return true;

    }

}