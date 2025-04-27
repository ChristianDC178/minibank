using MiniBank.CustomersSrv.Domain.Entities;
using MiniBank.CustomersSrv.Domain.Repositories;
using MiniBank.Exceptions;
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
            await customerDbContext.Collection.InsertOneAsync(customer, null, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            throw new MinibankRepositoryException("There is an error saving the customer", ex);
        }
    }

    public async Task<Customer> GetById(Guid customerId, CancellationToken cancellationToken)
    {
        try
        {
            var filter = Builders<Customer>.Filter.Eq(c => c.EntityId, customerId);
            var customer = await customerDbContext.Collection.Find(filter).FirstOrDefaultAsync();

            return customer;
        }
        catch (Exception ex)
        {
            throw new MinibankRepositoryException($"There is an error retrieving the customer by Id. Customer Id: {customerId}", ex);
        }
    }


    public async Task<Customer> GetByDocument(Document document, CancellationToken cancellationToken)
    {
        try
        {
            var documentIdFilter = Builders<Customer>.Filter.Eq(C => C.Document.DocumentId, document.DocumentId);
            var documentType = Builders<Customer>.Filter.Eq(C => C.Document.Type, document.Type);

            var filter = Builders<Customer>.Filter.And(documentIdFilter, documentType);
            var customer = await customerDbContext.Collection.Find(filter).FirstOrDefaultAsync();

            return customer;
        }
        catch (Exception ex)
        {
            throw new MinibankRepositoryException($"There is an error retrieving the customer by document. Document Id: {document?.DocumentId}", ex);
        }
    }

    public async Task<bool> Update(Customer customer, CancellationToken cancellationToken)
    {
        try
        {

            var replacementResult = await customerDbContext.Collection.ReplaceOneAsync<Customer>((c) =>
                                 c.EntityId == customer.EntityId, customer, cancellationToken: cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            throw new MinibankRepositoryException($"There is an error updating the customer. Customer Id: {customer?.EntityId}", ex);
        }
    }

}