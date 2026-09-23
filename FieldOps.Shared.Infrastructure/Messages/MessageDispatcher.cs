using FieldOps.Shared.Abstractions.Messages;
using MediatR;

namespace FieldOps.Shared.Infrastructure.Messages;

internal class MessageDispatcher(ISender sender) : IMessageDispatcher
{
    public Task<TResult> Send<TResult>(IMessage<TResult> message, CancellationToken ct = default)
    {
        return sender.Send(message, ct);
    }

    public Task Send(IMessage message, CancellationToken ct = default)
    {
        return sender.Send(message, ct);
    }
}
