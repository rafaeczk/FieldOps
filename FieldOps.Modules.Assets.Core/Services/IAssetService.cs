using FieldOps.Modules.Assets.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.Services
{
    public interface IAssetService
    {
        Task<Guid> CreateAsync(CreateAssetDto dto);
        Task<AssetDetailsDto?> GetByAsync(Guid id);
        Task<IReadOnlyList<AssetDto>> BrowseAsync();
        Task UpdateAsync(Guid id, EditAssetDto dto);
        Task DeleteAsync(Guid id);

    }
}
