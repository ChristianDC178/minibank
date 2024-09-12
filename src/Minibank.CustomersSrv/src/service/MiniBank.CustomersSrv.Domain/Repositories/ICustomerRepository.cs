using MiniBank.CustomersSrv.Domain.Entities;

namespace MiniBank.CustomersSrv.Domain.Repositories;

public interface ICustomerRepository
{
    Task<bool> Save(Customer customer, CancellationToken cancellationToken);
    Task<bool> Update(Customer customer, CancellationToken cancellationToken);
    Task<Customer> GetById(Guid customerId, CancellationToken cancellationToken);
    Task<bool> Replace(Customer customer, CancellationToken cancellationToken);
}
