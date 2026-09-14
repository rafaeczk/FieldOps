using FieldOps.Modules.Assets.Core.Entities;
using FieldOps.Modules.Assets.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace FieldOps.Modules.Assets.Core.DAL.Repositories;

internal class AssetRepository(AssetsDbContext context) : IAssetRepository
{
    private readonly AssetsDbContext context = context;

    public Task CreateAsync(Asset asset)
    {
        context.Assets.Add(asset);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Asset asset)
    {
        context.Assets.Update(asset);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Asset asset)
    {
        asset.SoftDelete(DateTime.UtcNow);
        context.Assets.Update(asset);
        return Task.CompletedTask;
    }

    public async Task<IReadOnlyList<Asset>> BrowseAsync()
    {
        return await context.Assets.AsNoTracking().Where(a => !a.IsDeleted).ToListAsync();
    }

    public Task<Asset?> GetAsync(Guid id)
        => context.Assets.FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);

    public async Task<int> CountAsync(Expression<Func<Asset, bool>> predicate, CancellationToken ct = default)
    {
        return await context.Assets.CountAsync(predicate, ct);
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        => context.Assets.AnyAsync(a => a.Id == id && !a.IsDeleted, ct);
}

