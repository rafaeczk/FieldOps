namespace FieldOps.Shared.Abstractions.Events;

public interface IEventHandler<in Event>
    where Event : class, IEvent
{
    Task HandleAsync(Event @event);
}
