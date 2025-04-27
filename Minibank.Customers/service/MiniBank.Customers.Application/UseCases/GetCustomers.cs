using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

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


            customer.LastName = "Cristiano";
            customer.FirstName = "Ronaldo";

            var updateResult = await customerRepository.Replace(customer, cancellationToken);


            return customer.FirstName;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
