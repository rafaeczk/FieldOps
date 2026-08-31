using FieldOps.Modules.Assets.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.Services
{
    internal class AssetService : IAssetService
    {
        public Task<IReadOnlyList<AssetDto>> BrowseAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Guid> CreateAsync(CreateAssetDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<AssetDetailsDto?> GetByAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
