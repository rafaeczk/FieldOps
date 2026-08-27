using FieldOps.Modules.Files.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.Files.Core.DAL.Configurations;

internal class StoredFileConfiguration : IEntityTypeConfiguration<StoredFile>
{
    public void Configure(EntityTypeBuilder<StoredFile> builder)
    {
        builder.HasKey(f => f.Id);

        builder.Property(f => f.StorageKey).IsRequired().HasMaxLength(255);
        builder.HasIndex(f => f.StorageKey).IsUnique();

        builder.Property(f => f.FileName).IsRequired().HasMaxLength(255);

        builder.Property(f => f.ContentType).IsRequired().HasMaxLength(100);
    }
}
