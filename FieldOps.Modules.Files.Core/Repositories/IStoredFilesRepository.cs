using FieldOps.Modules.Files.Core.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Files.Core.Repositories;

internal interface IStoredFilesRepository
{
    void Add(StoredFile storedFile);
    Task<StoredFile?> GetAsync(FileId fileId);
    void Delete(StoredFile storedFile);
}
