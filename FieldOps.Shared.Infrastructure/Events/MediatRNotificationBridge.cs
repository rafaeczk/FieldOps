using FieldOps.Shared.Abstractions.Events;
using MediatR;

namespace FieldOps.Shared.Infrastructure.Events;

internal class MediatRNotificationBridge<Event>(IEnumerable<IIntegrationEventHandler<Event>> handlers) : INotificationHandler<Event>
    where Event : IIntegrationEvent
{
    public async Task Handle(Event @event, CancellationToken ct)
        => await Task.WhenAll(handlers.Select(h => h.HandleAsync(@event, ct)));
}
