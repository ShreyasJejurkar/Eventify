using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Eventify.Extensions.Microsoft.DependencyInjection
{
    public static class EventifyServiceExtensions
    {
        public static void AddEventify(this IServiceCollection services, params Assembly[] assemblies)
        {
            if (assemblies.Length == 0)
            {
                throw new Exception("Please provide at least one assembly to scan for EventHandlers");
            }
            
            foreach (var assembly in assemblies)
            {
                var classTypes = assembly.GetTypes().Select(t => t.GetTypeInfo()).Where(t => t.IsClass && !t.IsAbstract);

                foreach (var type in classTypes)
                {
                    var interfaces = type.ImplementedInterfaces.Select(i => i.GetTypeInfo());

                    foreach (var handlerInterfaceType in interfaces.Where(i =>
                        i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEventHandler<>)))
                    {
                        services.AddScoped(handlerInterfaceType.AsType(), type.AsType());
                    }
                }
            }

            services.AddScoped<IEventPublisher, InMemoryEventPublisher>();
        }
    }
}