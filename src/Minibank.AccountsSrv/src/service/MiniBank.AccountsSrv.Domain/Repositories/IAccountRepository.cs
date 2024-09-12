using MiniBank.AccountsSrv.Domain.Entities;

namespace MiniBank.AccountsSrv.Domain.Repositories;

public interface IAccountRepository
{
    Task<bool> SaveCustomer(Account account, CancellationToken cancellationToken);
    Task<bool> UpdateCustomer(Account account, CancellationToken cancellationToken);
}
