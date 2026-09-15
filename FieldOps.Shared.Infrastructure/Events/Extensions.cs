using FieldOps.Shared.Abstractions.Events;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Shared.Infrastructure.Events;

public static class Extensions
{
    public static IServiceCollection AddMediatRNotificationHandlers(this IServiceCollection services, Type whereToLook)
    {
        var allTypes = whereToLook.Assembly.GetTypes();

        foreach (var type in allTypes)
        {
            var interfaces = type.GetInterfaces().Where(i => i.IsGenericType);

            foreach (var @interface in interfaces)
            {
                var genericDefinition = @interface.GetGenericTypeDefinition();

                if (genericDefinition == typeof(IIntegrationEventHandler<>))
                {
                    var messageType = @interface.GenericTypeArguments[0];

                    services.AddTransient(@interface, type);

                    var mediatRInterface = typeof(INotificationHandler<>).MakeGenericType(messageType);

                    var closedBridgeType = typeof(MediatRNotificationBridge<>).MakeGenericType(messageType);

                    services.AddTransient(mediatRInterface, closedBridgeType);
                }
            }
        }

        return services;
    }
}
