using FieldOps.Modules.Files.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Files.Core.Features.Queries
{
    public record CheckFileId(Guid FileId) : IMessage<bool>;
    internal class CheckFileIdHandler(IStoredFilesRepository repository) : IMessageHandler<CheckFileId, bool>
    {
        public async Task<bool> HandleAsync(CheckFileId message, CancellationToken ct)
        {
            return await repository.ExistsAsync(message.FileId, ct);
        }
    }
}

