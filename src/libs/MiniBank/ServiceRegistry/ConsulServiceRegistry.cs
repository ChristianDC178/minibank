using Consul;

namespace MiniBank.ServiceRegistry;

public class ConsulServiceRegistry : IServiceRegistry
{

    public async Task<ServiceInformation> GetServiceAsync(string serviceName)
    {
        ServiceInformation serviceInformation = null;

        Action<ConsulClientConfiguration> consulConfiguration = (config) =>
        {
            config.Address = new Uri("http://localhost:8500");
        };

        using (var consulClient = new ConsulClient(consulConfiguration))
        {
            var services = await consulClient.Agent.Services();
            if (services.Response.TryGetValue(serviceName, out AgentService service))
            {
                serviceInformation = new ServiceInformation(service.ID, service.Service, service.Address, service.Port);
            }
        }

        return serviceInformation;

    }

    public async Task RegisterInServiceRegistryAsync(ServiceInformation serviceInformation)
    {

        Action<ConsulClientConfiguration> consulConfiguration = (config) =>
        {
            config.Address = new Uri("http://localhost:8500");
        };

        var serviceId = "customer-srv";
        var serviceName = "Customer Service";

        using (var consulClient = new ConsulClient(consulConfiguration))
        {
            var registration = new AgentServiceRegistration()
            {
                ID = serviceId,
                Name = serviceName,
                Address = "localhost",
                Port = 5049,
                Check = new AgentServiceCheck
                {
                    HTTP = "http://localhost:5049/healthz",
                    Interval = TimeSpan.FromSeconds(10),
                    Timeout = TimeSpan.FromSeconds(5)
                }
            };

            await consulClient.Agent.ServiceRegister(registration);
        }
    }

}