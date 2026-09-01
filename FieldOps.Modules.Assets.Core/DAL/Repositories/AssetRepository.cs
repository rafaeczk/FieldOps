using FieldOps.Modules.Assets.Core.Entities;
using FieldOps.Modules.Assets.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace FieldOps.Modules.Assets.Core.DAL.Repositories;

internal class AssetRepository(AssetsDbContext context) : IAssetRepository
{
    private readonly AssetsDbContext context = context;

    public async Task CreateAsync(Asset asset)
    {
        await context.Assets.AddAsync(asset);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Asset asset)
    {
        context.Assets.Remove(asset);
        await context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Asset>> BrowseAsync()
    {
        return await context.Assets.AsNoTracking().ToListAsync();
    }

    public async Task<Asset?> GetAsync(Guid id)
    {
        return await context.Assets.FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<int> CountAsync(Expression<Func<Asset, bool>> predicate, CancellationToken ct = default)
    {
        return await context.Assets.CountAsync(predicate, ct);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
    {
        return await context.Assets.AnyAsync(a => a.Id == id, ct);
    }
}

