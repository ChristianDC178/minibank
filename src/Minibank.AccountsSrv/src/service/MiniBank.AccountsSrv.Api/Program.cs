using MiniBank.AccountsSrv.Api.Endpoints;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateSlimBuilder(args);

builder.Services.ConfigureHttpJsonOptions(options =>
{
    //options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(config =>
{
    config.DocumentName = "MiniBankAPI";
    config.Title = "MiniBankAPI v1";
    config.Version = "v1";
});


builder.Services.AddControllers();

builder.Services.AddMemoryCache();

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

//[JsonSerializable(typeof(Cus[]))]
//internal partial class AppJsonSerializerContext : JsonSerializerContext
//{

//}
