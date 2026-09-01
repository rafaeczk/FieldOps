using FieldOps.Modules.Files.Contracts;
using FieldOps.Modules.Files.Core.Features.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Files.Core.Services
{
    internal class FilesModuleApi(ISender sender) : IFilesModuleApi
    {
        private readonly ISender sender = sender;

        public Task<bool> AllExistAsync(IEnumerable<Guid> fileIds, CancellationToken ct = default)
        {
            return sender.Send(new CheckFileIds(fileIds));
        }
    }
}
