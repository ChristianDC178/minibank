using MiniBank.CustomersSrv.Domain.Entities;
using MiniBank.CustomersSrv.Domain.Repositories;
using MiniBank.MongoDB;
using MongoDB.Driver;

namespace MiniBank.CustomersSrv.Infrastructure.Database;

public class CustomerRepository
(
   IMongoDbDatabaseContext<Customer> customerDbContext
)
: ICustomerRepository
{

    public async Task<bool> Save(Customer customer, CancellationToken cancellationToken)
    {
        try
        {
            InsertOneOptions insertOneOptions = new InsertOneOptions();
            insertOneOptions.Comment = "Inserting a new person";

            await customerDbContext.Collection.InsertOneAsync(customer, insertOneOptions, cancellationToken);

            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Customer> GetById(Guid customerId, CancellationToken cancellationToken)
    {

        var filter = Builders<Customer>.Filter.Eq(c => c.EntityId, customerId);
        var customer = await customerDbContext.Collection.Find(filter).FirstOrDefaultAsync();

        return customer;
    }


    public async Task<Customer> GetByDocument(Document document, CancellationToken cancellationToken)
    {

        var documentIdFilter = Builders<Customer>.Filter.Eq(C => C.Document.DocumentId, document.DocumentId);
        var documentType = Builders<Customer>.Filter.Eq(C => C.Document.Type, document.Type);

        var filter = Builders<Customer>.Filter.And(documentIdFilter, documentType);
        var customer = await customerDbContext.Collection.Find(filter).FirstOrDefaultAsync();

        return customer;
    }


    public async Task<bool> Update(Customer customer, CancellationToken cancellationToken)
    {

        var filter = Builders<Customer>.Filter.Eq(c => c.EntityId, customer.EntityId);

        var update = Builders<Customer>.Update
            .Set(c => c.FirstName, customer.FirstName)
            .Set(c => c.LastName, customer.LastName)
            .Set(c => c.Document, customer.Document);



        var updateResult = await customerDbContext.Collection.UpdateOneAsync(filter, update);

        return true;

    }

    public async Task<bool> Replace(Customer customer, CancellationToken cancellationToken)
    {

        var replacementResult = await customerDbContext.Collection.ReplaceOneAsync<Customer>((c) =>
                                c.EntityId == customer.EntityId, customer, cancellationToken: cancellationToken);

        return true;

    }

}