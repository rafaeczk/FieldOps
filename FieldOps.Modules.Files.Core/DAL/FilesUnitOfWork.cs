using FieldOps.Modules.Files.Core.Repositories;

namespace FieldOps.Modules.Files.Core.DAL;

internal class FilesUnitOfWork(FilesDbContext context) : IFilesUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
