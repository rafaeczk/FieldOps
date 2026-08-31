using FieldOps.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Shared.Abstractions.Kernel.Ids
{
    public class AssetId : TypeId
    {
        public AssetId(Guid value) : base(value) { }

        private AssetId() : base(Guid.Empty) { }
    }
}
