using FieldOps.Modules.Files.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Files.Core.Features.Queries
{
    public record CheckFileIds(IEnumerable<Guid> FileIds) : IMessage<bool>;
    internal class CheckFileIdsHandler(IStoredFilesRepository repository) : IMessageHandler<CheckFileIds, bool>
    {
        public async Task<bool> HandleAsync(CheckFileIds message, CancellationToken ct)
        {
            var distinctIds = message.FileIds?.Distinct().ToList() ?? new List<Guid>();

            if (!distinctIds.Any())
            {
                return true;
            }

            var existingCount = await repository.CountAsync(f => distinctIds.Contains(f.Id), ct);

            return existingCount == distinctIds.Count;
        }
    }
}
