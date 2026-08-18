namespace FieldOps.Shared.Infrastructure.Messaging;

public sealed class MessageBroadcastRegistration(Type receiverType, Func<object, Task> action)
{
    public Type ReceivingType { get; } = receiverType;
    public Func<object, Task> Action { get; } = action;
    public string Key => ReceivingType.Name;
}
