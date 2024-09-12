using MongoDB.Bson.Serialization;
using System.Reflection;

namespace MiniBank.MongoDB;

public class RegisterClassMapBuilder
{

    public static void Register(Assembly assembly)
    {
        ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

        var baseTypeDefinition = typeof(BsonClassMapBuilder<>);

        var types = assembly.GetTypes()
                            .Where(t => t.BaseType != null &&
                                        t.BaseType.Name == baseTypeDefinition.Name)
                            .ToList();

        foreach (var type in types)
        {
            var methodInfo = type.GetMethod("RegisterClassMap");
            var fieldInfo = type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(f => f.Name == "map");

            if (methodInfo == null)
            {
                continue;
            }

            var instance = Activator.CreateInstance(type);

            if (instance == null)
            {
                continue;
            }

            var bsonMapClass = methodInfo.Invoke(instance, null);

            try
            {
                methodInfo.Invoke(instance, null);

                var bsonMapClassResult = fieldInfo.GetValue(instance);

                if (bsonMapClassResult is BsonClassMap mapClass && !BsonClassMap.IsClassMapRegistered(type))
                {
                    BsonClassMap.RegisterClassMap(mapClass);
                }
            }
            catch (TargetInvocationException ex)
            {
                throw new BsonClassMapRegistrationException($"Error invoking RegisterClassMap on type {type.FullName}: {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                throw new BsonClassMapRegistrationException($"Unepected error {ex.Message}");
            }
        }

    }

}
