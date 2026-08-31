using FieldOps.Modules.Assets.Core.Entities;
using FieldOps.Modules.Assets.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.DAL.Repositories
{
    internal class AssetRepository : IAssetRepository
    {
        public Task<IReadOnlyList<Asset>> BrowseAsync()
        {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Asset asset)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Asset asset)
        {
            throw new NotImplementedException();
        }

        public Task<Asset?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
