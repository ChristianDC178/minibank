using MediatR;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.CustomersSrv.Domain.Repositories;
using MiniBank.CustomersSrv.Application.Dtos.Validators;
using MiniBank.ServiceRegistry;
using FluentValidation;
using Microsoft.Extensions.Logging;
using MiniBank;
using MiniBank.ResultPattern;

namespace MiniBank.CustomersSrv.Application.UseCases;

public class CreateCustomerAddressUseCase
(
    ICustomerRepository customerRepository,
    IValidator<CreateCustomerAddressRequest> CreateCustomerAddressRequestValidator,
    ILogger<CreateCustomerAddressUseCase> logger
)
: IRequestHandler<CreateCustomerAddressRequest, Result<CustomerResponse>>
{

    public async Task<Result<CustomerResponse>> Handle(CreateCustomerAddressRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validation = CreateCustomerAddressRequestValidator.Validate(request);

            if (!validation.IsValid)
            {
                return Result.Failure(validation.Errors.First().ErrorMessage);
            }

            var customer = await customerRepository.GetById(request.CustomerId, cancellationToken);

            if (customer == null)
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

            return Result.Success<CustomerResponse>(new CustomerResponse() { });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

}
