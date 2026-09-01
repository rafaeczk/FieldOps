using FieldOps.Modules.Assets.Contracts;
using FieldOps.Modules.Assets.Core.Features.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FieldOps.Modules.Assets.Core.Services
{
    internal class AssetsModuleApi(ISender sender) : IAssetsModuleApi
    {
        private readonly ISender sender = sender;

        public Task<bool> Exists(Guid assetId, CancellationToken ct = default)
        {

            return sender.Send(new CheckAssetId(assetId), ct);
        }
    }
}
