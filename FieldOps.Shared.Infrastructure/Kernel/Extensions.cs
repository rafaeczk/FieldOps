using FieldOps.Shared.Abstractions.Kernel;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Shared.Infrastructure.Kernel;

public static class Extensions
{
    internal static IServiceCollection AddDomainEvents(this IServiceCollection services)
    {
        services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
        return services;
    }

    public static IServiceCollection AddDomainEventHandlers(this IServiceCollection services, Type whereToLook)
    {
        var allTypes = whereToLook.Assembly.GetTypes();

        foreach (var type in allTypes)
        {
            var interfaces = type.GetInterfaces().Where(i => i.IsGenericType);

            foreach(var @interface in interfaces)
            {
                var genericDefinition = @interface.GetGenericTypeDefinition();

                if(genericDefinition == typeof(IDomainEventHandler<>))
                {
                    services.AddTransient(@interface, type);
                }
            }
        }

        return services;
    }
}
