using FieldOps.Modules.Assets.Core.DTOs;
using FieldOps.Modules.Assets.Core.Entities;
using FieldOps.Modules.Assets.Core.Exceptions;
using FieldOps.Modules.Assets.Core.Repositories;
using FieldOps.Shared.Abstractions.Time;
using System.Linq;

namespace FieldOps.Modules.Assets.Core.Services;

internal class AssetService(IAssetRepository repository, IAssetUnitOfWork unitOfWork, IClock clock) : IAssetService
{
    private readonly IAssetRepository repository = repository;
    private readonly IAssetUnitOfWork unitOfWork = unitOfWork;
    private readonly IClock clock = clock;

    public async Task<Guid> CreateAsync(CreateAssetDto dto)
    {
        var createdAt = clock.UtcNow();

        var asset = Asset.Create(dto.Name, dto.SerialNumber, dto.Model, dto.Manufacturer, dto.PurchaseDate, dto.WarrantyExpires, createdAt);

        await repository.CreateAsync(asset);
        await unitOfWork.SaveChangesAsync();

        return asset.Id;
    }

    public async Task<AssetDetailsDto?> GetByAsync(Guid id)
    {
        var asset = await repository.GetAsync(id);
        if (asset is null) return null;

        return new AssetDetailsDto(asset.Id, asset.Name, asset.SerialNumber, asset.Model, asset.Manufacturer, asset.PurchaseDate, asset.WarrantyExpires, asset.LastServiceDate, asset.Status.ToString(), asset.Notes);
    }

    public async Task<IReadOnlyList<AssetDto>> BrowseAsync()
    {
        var list = await repository.BrowseAsync();
        return list.Select(a => new AssetDto(a.Id, a.Name, a.Manufacturer, a.SerialNumber)).ToList();
    }

    public async Task UpdateAsync(Guid id, EditAssetDto dto)
    {
        var asset = await repository.GetAsync(id);
        if (asset is null)
            throw new AssetNotFoundException(id);

        asset.UpdateDetails(dto.Name, dto.SerialNumber, dto.Model, dto.Manufacturer, dto.PurchaseDate, dto.WarrantyExpires, dto.Notes, clock.UtcNow());

        await repository.UpdateAsync(asset);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var asset = await repository.GetAsync(id);
        if (asset is null)
            throw new AssetNotFoundException(id);

        await repository.DeleteAsync(asset);
        await unitOfWork.SaveChangesAsync();
    }
}
