using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.DTOs
{
    public record CreateAssetDto(
        string Name,
        string SerialNumber,
        string Model,
        string Manufacturer,
        DateTime? PurchaseDate,
        DateTime? WarrantyExpires,
        DateTime? LastServiceDate,
        string Status = "Active",
        string? Notes = null
    );
}
