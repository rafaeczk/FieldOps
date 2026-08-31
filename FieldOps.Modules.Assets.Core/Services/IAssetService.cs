using FieldOps.Modules.Assets.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.Services
{
    internal interface IAssetService
    {
        Task<Guid> CreateAsync(CreateAssetDto dto);
        Task<AssetDetailsDto?> GetByAsync(Guid id);
        Task<IReadOnlyList<AssetDto>> BrowseAsync();
        Task DeleteAsync(Guid id);

    }
}
