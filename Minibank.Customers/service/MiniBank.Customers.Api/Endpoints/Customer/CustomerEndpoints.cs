using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;

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
            .Produces<CreateCustomerResponse>(201)
            //.ProducesValidationProblem(404, contentType: "application/json")
            .WithOpenApi();

        customerApi.MapPost("/", CreateCustomer);
        customerApi.MapPost("/{customerId}", CreateCustomerAddress);

        return app;

    }

    public static async Task<Results<Ok<CreateCustomerResponse>, IResult>> CreateCustomer(
        CreateCustomerRequest request,
        IMediator mediator,
        CancellationToken cancellation)
    {

        int zero = 0;

        int r = 5 / zero;

        var result = await mediator.Send(request, cancellation);

        if (cancellation.IsCancellationRequested)
            return TypedResults.BadRequest();

        var result1 = TypedResults.StatusCode(200);

        return result is not null ? TypedResults.Ok(result.Payload) : TypedResults.BadRequest();
    }

    public static async Task<Results<Ok<string>, IResult>> GetCustomerById(
        Guid customerId,
        IMediator mediator,
        //ILogger logger,
        CancellationToken cancellation)
    {

        //logger.LogDebug("Logging debug");
        //logger.LogInformation("Logging Info");
        //logger.LogWarning("Logging Warning");
        //logger.LogError("Logging Error");
        //logger.LogCritical("Logging Critical");

        var customerIdRequest = new CustomerIdRequest()
        {
            CustomerId = customerId
        };

        var result = await mediator.Send(customerIdRequest, cancellation);

        if (cancellation.IsCancellationRequested)
            return TypedResults.BadRequest();

        return result is not null ? TypedResults.Ok("result.Payload") : TypedResults.NotFound();
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

    //public static async Task<IResult> GetCustomers(
    //  [FromQuery] string name, 
    //  IMediator mediator,
    //  CancellationToken cancellationToken)
    //{



    //    //var result = await mediator.Send(createCustomerRequest, cancellationToken);

    //    //return TypedResults.BadRequest();
    //}

}
