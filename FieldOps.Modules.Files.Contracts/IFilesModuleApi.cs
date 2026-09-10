using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Files.Contracts
{
    public interface IFilesModuleApi
    {
        Task<bool> AllExistAsync(IEnumerable<Guid> fileIds, CancellationToken ct = default);
        Task<bool> GetFileExists(Guid fileId, CancellationToken ct = default);

    }
}
