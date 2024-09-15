using MiniBank.CustomersSrv.Api.Endpoints;
using MiniBank.CustomersSrv.Application.DependencyInjection;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.CustomersSrv.Domain.Entities;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

try
{

    var builder = WebApplication.CreateSlimBuilder(args);

    builder.Services.ConfigureHttpJsonOptions(options =>
    {
        options.SerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddOpenApiDocument(config =>
    {
        config.DocumentName = "MiniBankAPI";
        config.Title = "MiniBankAPI v1";
        config.Version = "v1";
    });


    builder.Services.AddControllers();

    builder.Services.RegisterApplicationDependencies();

    builder.Services.AddSerilog((services, lc) => lc
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext());

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseOpenApi();
        app.UseSwaggerUi(config =>
        {
            config.DocumentTitle = "MiniBankAPI - Customers Service";
            config.Path = "/swagger";
            config.DocumentPath = "/swagger/{documentName}/swagger.json";
            config.DocExpansion = "list";
        });
    }

    app.MapControllers();

    app.AddMiniBankEndpoints();

    app.Run();

}
catch (Exception ex)
{
    throw;
}

[JsonSerializable(typeof(CreateCustomerRequest))]
[JsonSerializable(typeof(CreateCustomerResponse))]
[JsonSerializable(typeof(CreateCustomerAddressRequest))]
[JsonSerializable(typeof(Customer))]
internal partial class AppJsonSerializerContext : JsonSerializerContext { }