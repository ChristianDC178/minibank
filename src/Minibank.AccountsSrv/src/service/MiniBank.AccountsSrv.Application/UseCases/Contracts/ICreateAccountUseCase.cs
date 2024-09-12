using MiniBank.AccountsSrv.Application.Dtos.Requests;
using MiniBank.AccountsSrv.Application.Dtos.Responses;

namespace MiniBank.AccountsSrv.Application.UseCases.Contracts;

public interface ICreateAccountUseCase 
{
    public Task<CreateAccountResponse> CreateCustomer(CreateAccountRequest request, CancellationToken cancellationToken );

}
