namespace FieldOps.Shared.Infrastructure.Messaging;

public interface IMessageRegistry
{
    void AddBroadcastAction(Type eventType);
    IEnumerable<MessageBroadcastRegistration> GetBroadcastRegistrations(string key);
}
