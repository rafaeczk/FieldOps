using FieldOps.Modules.Files.Core.Entities;

namespace FieldOps.Modules.Files.Core.Repositories;

internal interface IStoredFilesRepository
{
    void Add(StoredFile storedFile);
    Task<StoredFile?> GetAsync(Guid fileId);
    void Delete(StoredFile storedFile);
}
