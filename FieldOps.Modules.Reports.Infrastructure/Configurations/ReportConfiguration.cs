using FieldOps.Modules.Reports.Domain.Reports.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Infrastructure.Configurations
{
    internal sealed class ReportConfiguration : IEntityTypeConfiguration<Report>
    {
        public void Configure(EntityTypeBuilder<Report> builder)
        {
            builder.ToTable("Reports");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .HasConversion(id => id.Value, value => new(value));

            builder.Property(r => r.JobId)
                .HasConversion(id => id.Value, value => new(value))
                .IsRequired();

            builder.Property(r => r.CreatorId)
                .HasConversion(id => id.Value, value => new(value))
                .IsRequired();

            builder.Property(r => r.AssetId);

            builder.OwnsOne(r => r.Address, addressBuilder =>
            {
                addressBuilder.Property(a => a.CountryCode).HasColumnName("CountryCode").HasMaxLength(10);
                addressBuilder.Property(a => a.PostalCode).HasColumnName("PostalCode").HasMaxLength(20);
                addressBuilder.Property(a => a.City).HasColumnName("City").HasMaxLength(100);
                addressBuilder.Property(a => a.Street).HasColumnName("Street").HasMaxLength(150);
                addressBuilder.Property(a => a.BuildingNumber).HasColumnName("BuildingNumber").HasMaxLength(20);
                addressBuilder.Property(a => a.ApartmentNumber).HasColumnName("ApartmentNumber").HasMaxLength(20).IsRequired(false);
            });

            builder.Property(r => r.Note)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(r => r.Version).IsConcurrencyToken();

            builder.OwnsMany(r => r.FileIds, a =>
            {
                a.ToTable("RaportAttachments");

                a.WithOwner().HasForeignKey("RaportId");

                a.Property(f => f.Value)
                    .HasColumnName("FileId")
                    .IsRequired();

                a.HasKey("RaportId", "Value");
            });

            builder.Navigation(r => r.FileIds)
                .UsePropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
