using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Shared.Abstractions.Kernel.Ids;

public class AssetId : TypeId
{
    public AssetId(Guid value) : base(value) { }

    private AssetId() : base(Guid.Empty) { }
}
