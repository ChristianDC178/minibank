using Microsoft.AspNetCore.Http.HttpResults;
using MiniBank.AccountsSrv.Application.Dtos.Requests;
using MiniBank.AccountsSrv.Application.Dtos.Responses;

namespace MiniBank.AccountsSrv.Api.Endpoints;

public static class AccountEndpoints
{

    public static WebApplication AddAccountEndpoints(this WebApplication app)
    {

        var customerApi = app.MapGroup("/accounts");

        customerApi.MapGet("/{accountId}", GetAccountById);
        customerApi.MapPost("/", CreateAccount);

        customerApi.RequireAuthorization();

        return app;

    }


    public static async Task<Results<Ok<CreateAccountResponse>, BadRequest>> CreateAccount(CreateAccountRequest accountRequest, HttpContext http, CancellationToken cancellation)
    {

        //if (cancellation.IsCancellationRequested)
        //    return TypedResults.BadRequest();

        //var a = customer;
        //a.Id = Guid.NewGuid();

        //await Task.FromResult(Task.CompletedTask);

        //return customer is not null ? TypedResults.Ok(customer) : TypedResults.BadRequest();

        //return TypedResults.Ok(customer);
        //return result;


        throw new NotImplementedException();
    }

    public static async Task<Results<Ok<CreateAccountResponse>, NotFound>> GetAccountById(Guid guid, HttpContext http, CancellationToken cancellation)
    {
        //var customer = new Customer()
        //{
        //    Id = Guid.NewGuid(),
        //    Name = "Di Costanzo"
        //};

        //await Task.FromResult(Task.CompletedTask);

        //return TypedResults.Ok(customer);

        throw new NotImplementedException();
    }

}
