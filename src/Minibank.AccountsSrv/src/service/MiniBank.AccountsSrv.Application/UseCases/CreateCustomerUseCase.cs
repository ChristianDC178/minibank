using MiniBank.AccountsSrv.Application.Dtos.Responses;
using MiniBank.AccountsSrv.Application.UseCases.Contracts;
using MiniBank.AccountsSrv.Domain.Repositories;
using MiniBank.AccountsSrv.Application.Dtos.Requests;

namespace MiniBank.AccountsSrv.Application.UseCases;

public class CreateAccountUseCase
(
IAccountRepository customerRepository
)
: ICreateAccountUseCase
{

    public async Task<CreateAccountResponse> CreateCustomer(CreateAccountRequest request, CancellationToken cancellationToken)
    {
        try
        {
            throw new NotImplementedException();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}

