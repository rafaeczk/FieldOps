namespace FieldOps.Shared.Abstractions.Kernel;

public interface IDomainEventHandler<in Event>
    where Event : class, IDomainEvent
{
    Task HandleAsync(Event @event);
}
