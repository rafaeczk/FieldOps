using FieldOps.Modules.Assets.Core.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.Repositories
{
    internal interface IAssetRepository
    {
        Task CreateAsync(Asset asset);
        Task<Asset?> GetAsync(Guid id);
        Task<IReadOnlyList<Asset>> BrowseAsync();
        Task DeleteAsync(Asset asset);
    }
}
