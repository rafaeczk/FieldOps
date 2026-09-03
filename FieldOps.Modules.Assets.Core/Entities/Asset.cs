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
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        private Asset() { }

        public static Asset Create(string name, string serialNumber, string model, string manufacturer, DateTime? purchaseDate, DateTime? warrantyExpires, DateTime createdAt)
        {
            return new Asset
            {
                Id = Guid.NewGuid(),
                Name = name,
                SerialNumber = serialNumber,
                Model = model,
                Manufacturer = manufacturer,
                PurchaseDate = purchaseDate,
                WarrantyExpires = warrantyExpires,
                CreatedAt = createdAt,
                UpdatedAt = createdAt
            };
        }

        public void UpdateDetails(string name, string serialNumber, string model, string manufacturer, DateTime? purchaseDate, DateTime? warrantyExpires, string notes, DateTime updatedAt)
        {
            Name = name;
            SerialNumber = serialNumber;
            Model = model;
            Manufacturer = manufacturer;
            PurchaseDate = purchaseDate;
            WarrantyExpires = warrantyExpires;
            Notes = notes;
            UpdatedAt = updatedAt;
        }

        public void SoftDelete(DateTime deletedAt)
        {
            if (IsDeleted) return;
            IsDeleted = true;
            DeletedAt = deletedAt;
            UpdatedAt = deletedAt;
        }

    }
}
