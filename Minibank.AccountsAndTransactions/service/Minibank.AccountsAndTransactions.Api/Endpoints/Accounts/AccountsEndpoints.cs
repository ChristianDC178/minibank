using Microsoft.AspNetCore.Http.HttpResults;
using MiniBank.ResultPattern;

namespace MiniBank.CustomersSrv.Api.Endpoints.Customer;

public static class CustomerEndpoints
{
    public static WebApplication AddCustomerEndpoints(this WebApplication app)
    {

        var customerApi = app.MapGroup("/accounts");

        customerApi
            .WithDisplayName("Customer Api");

        customerApi
            .MapGet("/{customerId}", GetAccountById)
            .WithName("GetAccountById")
            .WithSummary("Retrieve an account by Id");

        customerApi.MapPost("/", GetAccountById);

        return app;

    }


    public static async Task<Results<Ok<string>, IResult>> GetAccountById(
        CancellationToken cancellation)
    {

        throw new NotImplementedException();

        //var result = await mediator.Send(request, cancellation);

       // if (result.IsSuccess)
       // {
       //     return TypedResults.Ok(result.Payload);
       // }
       //
       // return TypedResults.BadRequest();
    }

}
