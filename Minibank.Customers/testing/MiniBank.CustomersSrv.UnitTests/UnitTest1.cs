using MiniBank.CustomersSrv.Infrastructure.Database.ClassMaps;
using System.Reflection;

namespace MiniBank.CustomersSrv.UnitTests;

public class UnitTest1
{
    [Fact]
    public void Can_Build_ClassMap()
    {

        var assembly = Assembly.GetAssembly(typeof(MiniBank.CustomersSrv.Infrastructure.Database.CustomerRepository));
        MiniBank.MongoDB.RegisterClassMapBuilder.Register(assembly);
    }
}