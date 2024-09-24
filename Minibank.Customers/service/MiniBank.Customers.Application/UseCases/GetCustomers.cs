using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Domain.Repositories;
using MediatR;

namespace MiniBank.CustomersSrv.Application.UseCases;

internal class GetCustomersUseCase
(
    ICustomerRepository customerRepository
) : IRequestHandler<CustomerIdRequest, string>
{
    public async Task<string> Handle(CustomerIdRequest request, CancellationToken cancellationToken)
    {

        var customer = await customerRepository.GetById(request.CustomerId, cancellationToken);


        customer.LastName = "Cristiano";
        customer.FirstName = "Ronaldo";

        var updateResult = await customerRepository.Replace(customer, cancellationToken);


        return customer.FirstName;
    }
}
