namespace MiniBank.ServiceRegistry;

public interface IServiceRegistry
{
    internal Task RegisterInServiceRegistryAsync(ServiceInformation serviceInformation);

    public Task<ServiceInformation> GetServiceAsync(string serviceName);
}
