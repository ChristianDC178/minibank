
using MiniBank.CustomersSrv.Api.Endpoints.Customer;

namespace MiniBank.CustomersSrv.Api.Endpoints;

public static class MiniBankEndpoints
{

    public static WebApplication AddMiniBankEndpoints(this WebApplication app)
    {
        app.AddCustomerEndpoints();

        return app;
    }

}
