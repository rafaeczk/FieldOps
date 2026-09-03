using System;

namespace FieldOps.Modules.Assets.Core.DTOs
{
    public record AssetDetailsDto(
        Guid Id,
        string Name,
        string SerialNumber,
        string Model,
        string Manufacturer,
        DateTime? PurchaseDate,
        DateTime? WarrantyExpires,
        DateTime? LastServiceDate,
        string Status,
        string Notes
    );
}
