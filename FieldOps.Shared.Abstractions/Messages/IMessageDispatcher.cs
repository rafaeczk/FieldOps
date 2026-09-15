namespace FieldOps.Shared.Abstractions.Messages;

public interface IMessageDispatcher
{
    Task<TResult> Send<TResult>(IMessage<TResult> message, CancellationToken ct = default);
    Task Send(IMessage message, CancellationToken ct = default);
}
