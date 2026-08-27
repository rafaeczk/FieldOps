namespace FieldOps.Shared.Abstractions.Messaging;

public interface IMessageClient
{
    Task PublishAsync<T>(T @event, CancellationToken ct = default) where T : class;
}
