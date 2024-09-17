using Elastic.Channels;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Ingest.Elasticsearch;
using Elastic.Serilog.Sinks;
using MiniBank.CustomersSrv.Api.Endpoints;
using MiniBank.CustomersSrv.Application.DependencyInjection;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Application.Dtos.Responses;
using MiniBank.CustomersSrv.Domain.Entities;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Elastic.Transport;

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


    builder.Services.AddHealthChecks();

    builder.Services.AddControllers();

    builder.Services.RegisterApplicationDependencies();

    builder.Services.AddSerilog((services, lc) =>
    {
        lc.ReadFrom.Configuration(builder.Configuration);
        lc.Enrich.FromLogContext();

        //lc.WriteTo.Elasticsearch(new[] { new Uri("http://localhost:9200") }, opts =>
        //{
        //    opts.DataStream = new DataStreamName("logs", "console-example", "demo");
        //    opts.BootstrapMethod = BootstrapMethod.Failure;
        //    opts.ConfigureChannel = channelOpts =>
        //    {
        //        //channelOpts.BufferOptions = new BufferOptions
        //        //{
        //        //    //ConcurrentConsumers = 10
        //        //};
        //    };
        //}, transport =>
        //{
        //    transport.Authentication(new BasicAuthentication("elastic", "elastic1234")); // Basic Auth
        //    // transport.Authentication(new ApiKey(base64EncodedApiKey)); // ApiKey
        //});
    });


    var app = builder.Build();

    app.MapHealthChecks("/healthz");

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