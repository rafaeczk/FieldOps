using FieldOps.Shared.Abstractions.Messaging;
using FieldOps.Shared.Infrastructure.Modules;

namespace FieldOps.Shared.Infrastructure.Messaging;

internal class MessageClient(IModuleSerializer serializer, IMessageRegistry registry) : IMessageClient
{
    private readonly IModuleSerializer serializer = serializer;
    private readonly IMessageRegistry registry = registry;

    public async Task PublishAsync(object message)
    {
        var key = message.GetType().Name;
        var registrations = registry.GetBroadcastRegistrations(key);
        var tasks = registrations.Select(r => r.Action(Translate(message, r.ReceivingType)));
        await Task.WhenAll(tasks);
    }

    private object Translate(object value, Type type)
        => serializer.Deserialize(serializer.Serialize(value), type);
}
