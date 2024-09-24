using Consul;
using StackExchange.Redis;

namespace MiniBank.ServiceRegistry;

public class ServiceInformation
(
    string Id, 
    string name,
    string address,
    int port
)
{

    public string Id
    {
        get => Id;
    }

    public string Name
    {
        get => name;
    }

    public string Address
    {
        get => address;
    }

    public int Port
    {
        get => port;
    }

}
