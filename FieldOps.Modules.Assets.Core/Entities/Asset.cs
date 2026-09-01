using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.Entities
{
    internal class Asset
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string SerialNumber { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public DateTime? PurchaseDate { get; set; }
        public DateTime? WarrantyExpires { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public AssetStatus Status { get; set; } = AssetStatus.Active;
        public string Notes { get; set; } = string.Empty;

    }
}
