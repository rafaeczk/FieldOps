namespace FieldOps.Shared.Abstractions.Messaging;

public interface IMessageClient
{
    Task PublishAsync(object message);
}
