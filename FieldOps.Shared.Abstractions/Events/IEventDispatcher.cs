namespace FieldOps.Shared.Abstractions.Events;

public interface IEventDispatcher
{
    Task PublishAsync<Event>(Event @event)
        where Event : class, IEvent;
}
