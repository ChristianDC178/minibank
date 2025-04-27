using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.ResultPattern;

namespace MiniBank.CustomersSrv.Api.Endpoints.Customer;

public static class CustomerEndpoints

{
    public static WebApplication AddCustomerEndpoints(this WebApplication app)
    {

        var customerApi = app.MapGroup("/customers");

        customerApi
            .WithDisplayName("Customer Api");

        customerApi
            .MapGet("/{customerId}", GetCustomerById)
            .WithName("GetCustomerById 1")
            .WithSummary("Es el endpoint que permite obtener los customer por ID")
            .Produces<CustomerResponse>(201);

        customerApi.MapPost("/", CreateCustomer);
        customerApi.MapPost("/{customerId}", UpdateCustomer);
        customerApi.MapPost("/{customerId}/addresses", CreateCustomerAddress);

        return app;

    }


    public static async Task<Results<Ok<CustomerResponse>, IResult>> CreateCustomer(
        CreateCustomerRequest request,
        IMediator mediator,
        CancellationToken cancellation)
    {

        var result = await mediator.Send(request, cancellation);

        if (result.IsSuccess)
        {
            return TypedResults.Ok(result.Payload);
        }

        return TypedResults.BadRequest();
    }

    public static async Task<IResult> CreateCustomerAddress(
        Guid customerId,
        CreateCustomerAddressRequest createCustomerRequest,
        IMediator mediator,
        CancellationToken cancellationToken)
    {

        createCustomerRequest.CustomerId = customerId;

        var result = await mediator.Send(createCustomerRequest, cancellationToken);

        return TypedResults.BadRequest();
    }

    public static async Task<IResult> UpdateCustomer(
        Guid customerId,
        UpdateCustomerRequest updateCustomerRequest,
        IMediator mediator,
        CancellationToken cancellation)
    {

        updateCustomerRequest.Id = customerId;

        var result = await mediator.Send(updateCustomerRequest, cancellation);
        return result is not null ? TypedResults.Ok("result.Payload") : TypedResults.NotFound();
    }

    public static async Task<Results<Ok<string>, IResult>> GetCustomerById(
       Guid customerId,
       IMediator mediator,
       CancellationToken cancellation)
    {

        var customerIdRequest = new CustomerIdRequest()
        {
            CustomerId = customerId
        };

        var result = await mediator.Send(customerIdRequest, cancellation);

        return result is not null ? TypedResults.Ok("result.Payload") : TypedResults.NotFound();
    }

}
