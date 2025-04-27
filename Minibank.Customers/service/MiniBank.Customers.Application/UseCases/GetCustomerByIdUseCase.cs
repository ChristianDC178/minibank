using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MiniBank.CustomersSrv.Application.UseCases;

internal class GetCustomerByIdUseCase
(
    ICustomerRepository customerRepository,
    ILogger<GetCustomerByIdUseCase> logger
) : IRequestHandler<CustomerIdRequest, string>
{
    public async Task<string> Handle(CustomerIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await customerRepository.GetById(request.CustomerId, cancellationToken);

            return customer.FirstName;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
