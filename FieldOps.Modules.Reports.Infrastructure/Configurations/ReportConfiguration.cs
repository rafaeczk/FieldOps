using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.Reports.Infrastructure.Configurations;

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

        builder.Property(r => r.Latitude)
            .IsRequired(false);

        builder.Property(r => r.Longitude)
            .IsRequired(false);

        builder.Property(r => r.SignatureFileId)
            .HasConversion(id => id != null ? (Guid)id : (Guid?)null, value => value.HasValue ? new FileId(value.Value) : null)
            .IsRequired(false);

        builder.Property(r => r.Version).IsConcurrencyToken();

        builder.OwnsMany(r => r.Attachments, a =>
        {
            a.ToTable("ReportAttachments");

            a.WithOwner().HasForeignKey("ReportId");

            a.Property(f => f.FileId)
                .HasConversion(id => id.Value, value => new FileId(value))
                .HasColumnName("FileId")
                .IsRequired();

            a.HasKey("ReportId", "FileId");
        });

        builder.Navigation(r => r.Attachments)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .AutoInclude();

        builder.OwnsMany(r => r.ReportAssets, ra =>
        {
            ra.ToTable("ReportAssets");

            ra.WithOwner().HasForeignKey("ReportId");

            ra.Property(a => a.AssetId)
                .HasConversion(id => id.Value, value => new AssetId(value))
                .HasColumnName("AssetId")
                .IsRequired();

            ra.HasKey("ReportId", "AssetId");
        });

        builder.Navigation(r => r.ReportAssets)
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .AutoInclude();
    }
}
