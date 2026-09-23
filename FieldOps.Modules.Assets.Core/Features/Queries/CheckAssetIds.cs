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
}
