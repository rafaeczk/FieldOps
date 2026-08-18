namespace FieldOps.Shared.Infrastructure.Messaging;

public sealed class MessageRegistryOptions
{
    public List<Type> BroadcastActionEventTypes { get; } = [];
}
