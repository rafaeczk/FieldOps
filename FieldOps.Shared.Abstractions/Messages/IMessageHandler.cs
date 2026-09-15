namespace FieldOps.Shared.Abstractions.Messages;

public interface IMessageHandler<in Message, Result>
    where Message : IMessage<Result>
{
    Task<Result> HandleAsync(Message message, CancellationToken ct);
}

public interface IMessageHandler<in Message>
    where Message : IMessage
{
    Task HandleAsync(Message message, CancellationToken ct);
}
