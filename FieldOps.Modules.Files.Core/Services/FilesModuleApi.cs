using FieldOps.Modules.Files.Contracts;
using FieldOps.Modules.Files.Core.Features.Queries;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Files.Core.Services
{
    internal class FilesModuleApi(IMessageDispatcher messageDispatcher) : IFilesModuleApi
    {
        public Task<bool> AllExistAsync(IEnumerable<Guid> fileIds, CancellationToken ct = default)
        {
            return messageDispatcher.Send(new CheckFileIds(fileIds), ct);
        }

        public Task<bool> GetFileExists(Guid fileId, CancellationToken ct = default)
        {
            return messageDispatcher.Send(new CheckFileId(fileId), ct);
        }
    }
}
