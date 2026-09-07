using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Shared.Abstractions.Kernel.Ids;

public class FileId(Guid value) : TypeId(value)
{
    public static implicit operator FileId(Guid id) => new(id);
    public static implicit operator Guid(FileId id) => id.Value;
}
