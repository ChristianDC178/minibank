using MiniBank.AccountsAndTransactions.Api.Endpoints;

public static class MiniBankEndpoints
{

    public static WebApplication AddMiniBankEndpoints(this WebApplication app)
    {
        app.AddCustomerEndpoints();

        return app;

    }

}
