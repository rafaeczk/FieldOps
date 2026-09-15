namespace FieldOps.Shared.Abstractions.Kernel.Types;

public abstract class TypeId<T>(T value) : IEquatable<TypeId<T>>
{
    public T Value { get; } = value;

    public abstract bool IsEmpty();

    public bool Equals(TypeId<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<T>.Default.Equals(Value, other.Value);
    }

    public override bool Equals(object? o)
    {
        if (o is null) return false;
        if (ReferenceEquals(this, o)) return true;
        if (o.GetType() != GetType()) return false;
        return Equals((TypeId<T>)o);
    }

    public override int GetHashCode()
    {
        return EqualityComparer<T>.Default.GetHashCode(Value!);
    }
}

public class TypeId(Guid value) : TypeId<Guid>(value)
{
    public TypeId() : this(Guid.NewGuid())
    {
    }

    public override bool IsEmpty() => Value == Guid.Empty;

    public static implicit operator Guid(TypeId id) => id.Value;
    public static implicit operator TypeId(Guid id) => new(id);

    public static bool operator ==(TypeId a, TypeId b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is not null && b is not null) return a.Value.Equals(b.Value);
        return false;
    }
    public static bool operator !=(TypeId a, TypeId b) => !(a == b);

    public override bool Equals(object? o)
    {
        return base.Equals(o);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
