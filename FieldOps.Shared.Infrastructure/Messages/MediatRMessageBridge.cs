using FieldOps.Shared.Abstractions.Messages;
using MediatR;

namespace FieldOps.Shared.Infrastructure.Messages;

internal class MediatRMessageBridge<TMessage, TResult>(IMessageHandler<TMessage, TResult> domainHandler) : IRequestHandler<TMessage, TResult>
    where TMessage : class, IMessage<TResult>
{
    private readonly IMessageHandler<TMessage, TResult> domainHandler = domainHandler;

    public Task<TResult> Handle(TMessage request, CancellationToken ct)
        => domainHandler.HandleAsync(request, ct);
}

internal class MediatRVoidMessageBridge<TMessage>(IMessageHandler<TMessage> domainHandler) : IRequestHandler<TMessage>
    where TMessage : class, IMessage
{
    private readonly IMessageHandler<TMessage> domainHandler = domainHandler;

    public async Task Handle(TMessage request, CancellationToken ct)
    {
        await domainHandler.HandleAsync(request, ct);
    }
}
