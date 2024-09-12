using MiniBank.CustomersSrv.Api.Endpoints;
using MiniBank.CustomersSrv.Application.DependencyInjection;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.CustomersSrv.Domain.Entities;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    //options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
    options.SerializerOptions.TypeInfoResolver = new DefaultJsonTypeInfoResolver();
    //options.JsonSerializerIsReflectionEnabledByDefault = true;
   
    
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


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi(config =>
    {
        config.DocumentTitle = "MiniBankAPI";
        config.Path = "/swagger";
        config.DocumentPath = "/swagger/{documentName}/swagger.json";
        config.DocExpansion = "list";
    });
}

app.MapControllers();

app.AddMiniBankEndpoints();

app.Run();


[JsonSerializable(typeof(CreateCustomerRequest))]
[JsonSerializable(typeof(CreateCustomerResponse))]
[JsonSerializable(typeof(CreateCustomerAddressRequest))]
[JsonSerializable(typeof(Customer))]
internal partial class AppJsonSerializerContext : JsonSerializerContext
{

}
