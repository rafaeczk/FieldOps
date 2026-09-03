namespace FieldOps.Shared.Abstractions.Kernel.Types;

public class AggregateId<T>(T value) : IEquatable<AggregateId<T>>
{
    public T Value { get; } = value;

    public bool Equals(AggregateId<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? o)
    {
        if (o is null) return false;
        if (ReferenceEquals(this, o)) return true;
        if (o.GetType() != this.GetType()) return false;
        return Equals((AggregateId<T>)o);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Value!);
    }
}

public class AggregateId(Guid value) : AggregateId<Guid>(value)
{
    public AggregateId() : this(Guid.NewGuid())
    {
    }

    public static implicit operator Guid(AggregateId id) => id.Value;
    public static implicit operator AggregateId(Guid id) => new(id);
}
