using Microsoft.Extensions.DependencyInjection;
using MiniBank.AccountsAndTransactions.Domain.Entities;
using MiniBank.AccountsAndTransactions.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniBank.AccountsAndTransactions.Application.DependencyInjection;

public static class ServiceExtensions
{
    public static void RegisterApplicationDependencies(this IServiceCollection services)
    {
        services.RegisterMongoDBClassMaps();
    }

    static void RegisterMongoDBClassMaps(this IServiceCollection services)
    {
        var assembly = Assembly.GetAssembly(typeof(DepositAccountRepository));
        MiniBank.MongoDB.RegisterClassMapBuilder.Register(assembly);
    }

}
