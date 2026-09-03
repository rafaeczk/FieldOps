using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Shared.Abstractions.Kernel.Ids;

public class AccountId(Guid value) : TypeId(value)
{
    public static implicit operator AccountId(Guid id) => new(id);
    public static implicit operator Guid(AccountId id) => id.Value;
}
