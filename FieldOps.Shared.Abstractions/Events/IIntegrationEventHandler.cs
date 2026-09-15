namespace FieldOps.Shared.Abstractions.Events;

public interface IIntegrationEventHandler<in Event>
    where Event : IIntegrationEvent
{
    Task HandleAsync(Event message, CancellationToken ct);
}
