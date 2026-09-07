using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Shared.Abstractions.Kernel.Ids;

public class OperatorId(Guid value) : TypeId(value)
{
    public static implicit operator OperatorId(Guid id) => new(id);
    public static implicit operator Guid(OperatorId id) => id.Value;
}
