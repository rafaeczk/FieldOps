using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Shared.Abstractions.Kernel.Ids;

public class TechnicianId(Guid value) : TypeId(value)
{
    public static implicit operator TechnicianId(Guid id) => new(id);
    public static implicit operator Guid(TechnicianId id) => id.Value;
}
