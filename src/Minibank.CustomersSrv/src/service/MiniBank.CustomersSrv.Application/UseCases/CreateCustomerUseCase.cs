using FluentValidation;
using MediatR;
using MiniBank.Cache;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.CustomersSrv.Domain.Entities;
using MiniBank.CustomersSrv.Domain.Repositories;
using MongoDB.Driver;


namespace MiniBank.CustomersSrv.Application.UseCases;

public class CreateCustomerUseCase
(
    ICustomerRepository customerRepository,
    IMinibankEntityCache<Customer> customersCache,
    IValidator<CreateCustomerRequest> requestValidator
)
: IRequestHandler<CreateCustomerRequest, Result<CreateCustomerResponse>>
{

    public async Task<Result<CreateCustomerResponse>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var validationResult = requestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                Result.Failure(validationResult.Errors.First().ErrorMessage);
            }

            //Traer el customer desde la base de datos por el documentid
            Document document = new Document()
            {
                DocumentId = request.Document.DocumentId,
                Type = request.Document.Type
            };

            Customer customer = new Customer(
                request.FirstName,
                request.LastName,
                document);

            await customerRepository.Save(customer, cancellationToken);

            var createCustomerResponse = new CreateCustomerResponse()
            {
                Id = customer.EntityId,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            return Result.Success(createCustomerResponse);

        }
        catch (Exception ex)
        {
            throw;
        }
    }

}

