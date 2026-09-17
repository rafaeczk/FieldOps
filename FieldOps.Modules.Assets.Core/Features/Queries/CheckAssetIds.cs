using FieldOps.Modules.Assets.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldOps.Modules.Assets.Core.Features.Queries
{
    public record CheckAssetId(Guid AssetId) : IMessage<bool>;

    internal class CheckAssetIdHandler(IAssetRepository repository) : IMessageHandler<CheckAssetId, bool>
    {
        public async Task<bool> HandleAsync(CheckAssetId message, CancellationToken ct)
        {
            if (message.AssetId == Guid.Empty)
            {
                return false;
            }

            return await repository.ExistsAsync(message.AssetId, ct);
        }
    }

    public record CheckAssetIds(IEnumerable<Guid> AssetIds) : IMessage<List<Guid>>;

    internal class CheckAssetIdsHandler(IAssetRepository repository) : IMessageHandler<CheckAssetIds, List<Guid>>
    {
        public async Task<List<Guid>> HandleAsync(CheckAssetIds message, CancellationToken ct)
        {
            var ids = message.AssetIds.Where(id => id != Guid.Empty).Distinct().ToList();
            var valid = new List<Guid>();

            foreach (var id in ids)
            {
                if (await repository.ExistsAsync(id, ct))
                {
                    valid.Add(id);
                }
            }

            return valid;
        }
    }
}
