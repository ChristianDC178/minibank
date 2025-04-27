using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using MiniBank.Cache;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.CustomersSrv.Domain.Entities;
using MiniBank.CustomersSrv.Domain.Repositories;
using MiniBank.ResultPattern;
using MiniBank.ServiceRegistry;
using MongoDB.Driver;

namespace MiniBank.CustomersSrv.Application.UseCases;

public class CreateCustomerUseCase
(
    ICustomerRepository customerRepository,
    IMinibankEntityCache<Customer> customersCache,
    IValidator<CreateCustomerRequest> requestValidator,
    ILogger<CreateCustomerUseCase> logger,
    IServiceRegistry serviceRegistry
)
: IRequestHandler<CreateCustomerRequest, Result<CustomerResponse>>
{

    public async Task<Result<CustomerResponse>> Handle(CreateCustomerRequest request, CancellationToken cancellationToken)
    {
        try
        {

            logger.LogInformation("Creating customer");

            var validationResult = requestValidator.Validate(request);

            if (!validationResult.IsValid)
            {
                return Result.Failure(validationResult.Errors.First().ErrorMessage);
            }

            //Traer el customer desde la base de datos por el documentid
            Document document = new Document()
            {
                DocumentId = request.Document.DocumentId,
                Type = request.Document.Type
            };

            var existentCustomer = await customerRepository.GetByDocument(document, cancellationToken);

            if (existentCustomer != null)
            {
                return Result.Failure("There is a customer with the same document id");
            }

            Customer customer = new Customer(
                request.FirstName,
                request.LastName,
                request.BirthDate,
                document);

            customer.CreatedDate = customer.UpdatedDate = DateTime.UtcNow;

            logger.LogInformation("Saving a new Customer in the database");

            await customerRepository.Save(customer, cancellationToken);

            var createCustomerResponse = new CustomerResponse()
            {
                Id = customer.EntityId,
                FirstName = customer.FirstName,
                LastName = customer.LastName
            };

            return Result.Success(createCustomerResponse);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            throw;
        }
    }

}

