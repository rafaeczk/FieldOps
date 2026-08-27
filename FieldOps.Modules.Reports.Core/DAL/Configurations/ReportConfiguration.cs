using FieldOps.Modules.Reports.Core.Entities;
using FieldOps.Modules.Reports.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.Json;

namespace FieldOps.Modules.Reports.Core.DAL.Configurations;

internal class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.WorkOrderId).IsRequired();

        builder.Property(r => r.TechnicianId).IsRequired();

        builder.Property(r => r.Note).HasMaxLength(2000);

        builder.Property(r => r.PhotoPaths)
            .HasColumnType("jsonb")
            .HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<List<string>>(v, (JsonSerializerOptions?)null) ?? new List<string>());

        builder.Property(r => r.Latitude).HasColumnType("double precision");

        builder.Property(r => r.Longitude).HasColumnType("double precision");

        builder.Property(r => r.SignaturePath).HasMaxLength(500);

        builder.Property(r => r.QrData).HasMaxLength(1000);

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion(v => v.Value, v => new SyncStatus(v));

        builder.Property(r => r.CreatedAt).IsRequired();

        builder.Property(r => r.UpdatedAt).IsRequired();

        builder.HasIndex(r => r.WorkOrderId);

        builder.HasIndex(r => r.TechnicianId);

        builder.HasIndex(r => r.Status);
    }
}
