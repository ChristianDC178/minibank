using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MiniBank.CustomersSrv.Infrastructure.Database;
using MiniBank.CustomersSrv.Domain.Repositories;
using System.Reflection.Emit;
using MiniBank.MongoDB;
using FluentValidation;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using System.ComponentModel;
using MiniBank.Cache;
using MiniBank.CustomersSrv.Infrastructure.Cache;
using MiniBank.CustomersSrv.Domain.Entities;


namespace MiniBank.CustomersSrv.Application.DependencyInjection;

public static class ServiceExtensions
{

    public static void RegisterApplicationDependencies(this IServiceCollection services)
    {
        services.RegisterMediatRInternal();
        services.RegisterRepositories();
        RegisterMongoDBClassMaps();
        services.RegisterMongoDBClient();
        services.RegisterValidators();
        services.RegisterRedisCacheClient();
    }

    static void RegisterMediatRInternal(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
    }

    static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomerRepository, CustomerRepository>();
    }

    static void RegisterMongoDBClassMaps()
    {
        var assembly = Assembly.GetAssembly(typeof(CustomerRepository));
        MiniBank.MongoDB.RegisterClassMapBuilder.Register(assembly);
    }

    static void RegisterMongoDBClient(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClientWrapper, MongoClientWrapper>();
    }

    static void RegisterRedisCacheClient(this IServiceCollection services)
    {
        services.AddSingleton<IRedisClientWrapper, RedisClientWrapper>();
        services.AddScoped<IMinibankEntityCache<Customer>, CustomersCache>();
    }

    static void RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCustomerRequest>();
    }

}

