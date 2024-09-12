
namespace MiniBank.AccountsSrv.Api.Endpoints;

public static class MiniBankEndpoints
{

    public static WebApplication AddMiniBankEndpoints(this WebApplication app)
    {
        app.AddAccountEndpoints();

        return app;
    }

}
