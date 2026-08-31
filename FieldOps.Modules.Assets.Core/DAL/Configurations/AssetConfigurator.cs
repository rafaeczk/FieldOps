using FieldOps.Modules.Assets.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.DAL.Configurations
{
    internal class AssetConfigurator : IEntityTypeConfiguration<Asset>
    {
        public void Configure(EntityTypeBuilder<Asset> builder)
        {
            builder.ToTable("Assets");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.SerialNumber)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(a => a.SerialNumber)
                .IsUnique();

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.Model)
                .HasMaxLength(100);

            builder.Property(a => a.Manufacturer)
                .HasMaxLength(100);

            builder.Property(a => a.Status)
                .IsRequired()
                .HasDefaultValue("Active")
                .HasMaxLength(50);

            builder.Property(a => a.Notes)
                .HasMaxLength(1000);
        }
    }

}
