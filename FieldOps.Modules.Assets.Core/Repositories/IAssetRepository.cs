using FieldOps.Modules.Assets.Core.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace FieldOps.Modules.Assets.Core.Repositories
{
    internal interface IAssetRepository
    {
        Task CreateAsync(Asset asset);
        Task<Asset?> GetAsync(Guid id);
        Task<IReadOnlyList<Asset>> BrowseAsync();
        Task<int> CountAsync(Expression<Func<Asset, bool>> predicate, CancellationToken ct = default);
        Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
        Task UpdateAsync(Asset asset);
        Task DeleteAsync(Asset asset);
    }
}
