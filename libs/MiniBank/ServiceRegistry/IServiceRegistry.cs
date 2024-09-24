using MongoDB.Bson.Serialization.Attributes;
using ThirdParty.BouncyCastle.Asn1;

namespace MiniBank.ServiceRegistry;

public interface IServiceRegistry
{
    internal Task RegisterInServiceRegistryAsync(ServiceInformation serviceInformation);

    public Task<ServiceInformation> GetServiceAsync(string serviceName);
}
