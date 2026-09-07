using FieldOps.Modules.Assets.Contracts;
using FieldOps.Modules.Assets.Core.Features.Queries;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Assets.Core.Services
{
    internal class AssetsModuleApi(IMessageDispatcher messageDispatcher) : IAssetsModuleApi
    {
        public Task<bool> Exists(Guid assetId, CancellationToken ct = default)
        {

            return messageDispatcher.Send(new CheckAssetId(assetId), ct);
        }
    }
}
