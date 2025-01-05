using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using MiniBank.Cache;
using MiniBank.CustomersSrv.Application.Dtos.Requests;
using MiniBank.CustomersSrv.Domain.Entities;
using MiniBank.CustomersSrv.Domain.Repositories;
using MiniBank.CustomersSrv.Infrastructure.Cache;
using MiniBank.CustomersSrv.Infrastructure.Database;
using MiniBank.MongoDB;
using System.Reflection;


namespace MiniBank.CustomersSrv.Application.DependencyInjection;

public static class ServiceExtensions
{

    public static void RegisterApplicationDependencies(this IServiceCollection services)
    {
        services.RegisterMediatRInternal();
        services.RegisterRepositories();
        services.RegisterMongoDBClassMaps();
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

    static void RegisterMongoDBClassMaps(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(CustomerRepository));
        MiniBank.MongoDB.RegisterClassMapBuilder.Register(assembly);
    }

    static void RegisterMongoDBClient(this IServiceCollection services)
    {
        services.AddSingleton<IMongoClientWrapper, MongoClientWrapper>();

        services.AddScoped<IMongoDbDatabaseContext<Customer>, MongoEntityDbContext<Customer>>((provider) => {
            var instance = provider.GetService<IMongoClientWrapper>();
            return new MongoEntityDbContext<Customer>(instance) { DatabaseName = "customer-srv", CollectionName = "customers" };
        });

        services.AddScoped<IMongoDbDatabaseContext<Address>, MongoEntityDbContext<Address>>((provider) => {
            var instance = provider.GetService<IMongoClientWrapper>();
            return new MongoEntityDbContext<Address>(instance) { DatabaseName = "customer-srv", CollectionName = "address" };
        });
    }

    static void RegisterRedisCacheClient(this IServiceCollection services)
    {
        services.AddSingleton<IRedisClientWrapper, RedisClientWrapper>();
        services.AddScoped<IMinibankEntityCache<Customer>, CustomersCache>();
        //services.AddScoped<IAbstractCache<object>, ConcretCache<object>>();

    }

    static void RegisterValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCustomerRequest>();
    }

}

