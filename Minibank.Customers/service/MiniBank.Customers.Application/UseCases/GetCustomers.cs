using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using MiniBank.ResultPattern;

namespace MiniBank.CustomersSrv.Application.UseCases;

internal class GetCustomersUseCase
(
    ICustomerRepository customerRepository,
    ILogger<GetCustomersUseCase> logger

) : IRequestHandler<CustomerIdRequest, string>
{
    public async Task<string> Handle(CustomerIdRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await customerRepository.GetById(request.CustomerId, cancellationToken);

            if (customer == null) {
                Result.Failure($"There is not a customer with id {request.CustomerId}");
            }

            //aca debe ir el dto del customer
            Result.Success(customer);

            return customer.FirstName;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
