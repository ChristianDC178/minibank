using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace MiniBank.ServiceRegistry;

public static class ServiceRegistryExtensions
{
    public static IServiceCollection RegisterConsulServiceDiscoveryProvider(this IServiceCollection services, string serviceId, string serviceName)
    {
        services.AddScoped<IServiceRegistry, ConsulServiceRegistry>();

        ConsulServiceRegistry consulServiceRegistry = new ConsulServiceRegistry();

        consulServiceRegistry.RegisterInServiceRegistryAsync(new ServiceInformation(serviceId, serviceName, "", 5049));

        return services;
    }



}
