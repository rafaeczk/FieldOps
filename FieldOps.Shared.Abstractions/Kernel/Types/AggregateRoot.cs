namespace FieldOps.Shared.Abstractions.Kernel.Types;

public abstract class AggregateRoot<T>
{
    public T Id { get; protected set; }
    public int Version { get; private set; }
    public IEnumerable<IDomainEvent> Events => events;

    private readonly List<IDomainEvent> events = [];
    private bool versionIncremented = false;

    protected void AddEvent(IDomainEvent @event)
    {
        if (events.Count == 0 && !versionIncremented)
        {
            Version++;
            versionIncremented = true;
        }

        events.Add(@event);
    }

    protected void ClearEvents() => events.Clear();

    protected void IncrementVersion()
    {
        if (events.Count == 0 && !versionIncremented)
        {
            Version++;
            versionIncremented = true;
        }
    }
}

public abstract class AggregateRoot : AggregateRoot<AggregateId>
{
}
