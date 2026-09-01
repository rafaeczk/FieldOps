using FieldOps.Modules.Files.Core.Entities;
using System.Linq.Expressions;

namespace FieldOps.Modules.Files.Core.Repositories;

internal interface IStoredFilesRepository
{
    void Add(StoredFile storedFile);
    Task<StoredFile?> GetAsync(Guid fileId);
    void Delete(StoredFile storedFile);
    Task<int> CountAsync(Expression<Func<StoredFile, bool>> predicate, CancellationToken ct = default);
}
