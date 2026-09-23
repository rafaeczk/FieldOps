using FieldOps.Modules.Assets.Core.Repositories;

namespace FieldOps.Modules.Assets.Core.DAL;

internal class AssetUnitOfWork(AssetsDbContext context) : IAssetUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
