using FieldOps.Modules.Files.Core.Entities;
using FieldOps.Modules.Files.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Files.Core.DAL.Repositories;

internal class StoredFilesRepository(FilesDbContext context) : IStoredFilesRepository
{
    private readonly FilesDbContext context = context;

    public void Add(StoredFile storedFile)
    {
        context.Files.Add(storedFile);
    }

    public Task<StoredFile?> GetAsync(Guid fileId)
    {
        return context.Files.SingleOrDefaultAsync(f => f.Id == fileId);
    }

    public void Delete(StoredFile storedFile)
    {
        context.Files.Remove(storedFile);
    }
}
