using FieldOps.Shared.Abstractions.Messages;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Shared.Infrastructure.Messages;

public static class Extensions
{
    public static IServiceCollection AddMediatRRequestHandlers(this IServiceCollection services, Type whereToLook)
    {
        var allTypes = whereToLook.Assembly.GetTypes();

        foreach (var type in allTypes)
        {
            var interfaces = type.GetInterfaces().Where(i => i.IsGenericType);

            foreach (var @interface in interfaces)
            {
                var genericDefinition = @interface.GetGenericTypeDefinition();

                // for IRequestHandler returning Task
                if (genericDefinition == typeof(IMessageHandler<>))
                {
                    var messageType = @interface.GenericTypeArguments[0];

                    services.AddTransient(@interface, type);

                    var mediatRInterface = typeof(IRequestHandler<>).MakeGenericType(messageType);

                    var closedBridgeType = typeof(MediatRVoidMessageBridge<>).MakeGenericType(messageType);

                    services.AddTransient(mediatRInterface, closedBridgeType);
                }

                // for IRequestHandler returning Task<T>
                if (genericDefinition == typeof(IMessageHandler<,>))
                {
                    var messageType = @interface.GenericTypeArguments[0];
                    var resultType = @interface.GenericTypeArguments[1];

                    services.AddTransient(@interface, type);

                    var mediatRInterface = typeof(IRequestHandler<,>).MakeGenericType(messageType, resultType);
                    var closedBridgeType = typeof(MediatRMessageBridge<,>).MakeGenericType(messageType, resultType);

                    services.AddTransient(mediatRInterface, closedBridgeType);
                }
            }
        }

        return services;
    }
}
