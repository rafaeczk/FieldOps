using FieldOps.Modules.Assets.Core.Entities;
using System;

namespace FieldOps.Modules.Assets.Core.DTOs;

using FieldOps.Modules.Assets.Core.Entities;

public record EditAssetDto(string Name, string SerialNumber, string Model, string Manufacturer, DateTime? PurchaseDate, DateTime? WarrantyExpires, DateTime? LastServiceDate, AssetStatus Status, string Notes);
