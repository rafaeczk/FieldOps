using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Contracts
{
    public interface IAssetsModuleApi
    {
        Task<bool> Exists(Guid assetId, CancellationToken ct = default);
    }
}
