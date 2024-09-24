using MediatR;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.CustomersSrv.Domain.Repositories;
using MiniBank.ServiceRegistry;

namespace MiniBank.CustomersSrv.Application.UseCases;

public class CreateCustomerAddressUseCase
(
    ICustomerRepository customerRepository
)
: IRequestHandler<CreateCustomerAddressRequest, Result<CreateCustomerResponse>>
{

    public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerAddressRequest request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetById(request.CustomerId, cancellationToken);

        if (customer is null)
        {
            Result.Failure("Customer not found");
        }

        customer.Address = new Domain.Entities.Address()
        {
            State = request.State,
            ZipCode = request.ZipCode,
            StreetName = request.StreetName
        };

        var updateResult = await customerRepository.Update(customer, cancellationToken);

        return Result.Success<CreateCustomerResponse>(new CreateCustomerResponse() { });

    }


}
