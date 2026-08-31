using FieldOps.Shared.Abstractions.Events;
using MediatR;

namespace FieldOps.Shared.Infrastructure.Events;

internal class MediatRNotificationBridge<Event>(IIntegrationEventHandler<Event> domainHandler) : INotificationHandler<Event>
    where Event : IIntegrationEvent
{
    private readonly IIntegrationEventHandler<Event> domainHandler = domainHandler;

    public Task Handle(Event @event, CancellationToken ct)
        => domainHandler.HandleAsync(@event, ct);
}
