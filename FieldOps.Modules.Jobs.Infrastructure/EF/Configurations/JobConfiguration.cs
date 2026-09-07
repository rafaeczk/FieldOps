using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Shared.Abstractions.Kernel.Types;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Configurations;

internal class JobConfiguration : IEntityTypeConfiguration<Job>
{
    public void Configure(EntityTypeBuilder<Job> builder)
    {
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Id).HasConversion(x => x.Value, x => new(x));

        builder.Property(j => j.Version).IsConcurrencyToken();

        builder.Property(j => j.CreatorId).IsRequired().HasConversion(x => x.Value, x => new(x));

        builder.Property(j => j.Title).IsRequired();

        builder.Property(j => j.Description).IsRequired(false);

        builder.Property(j => j.Status).IsRequired().HasConversion(x => x.Value, x => new(x));

        builder.Property(j => j.Priority).IsRequired().HasConversion(x => x.Value, x => new(x));

        builder.OwnsOne(x => x.Address, address =>
        {
            address.Property(a => a.CountryCode)
                   .HasColumnName("Address_CountryCode")
                   .HasMaxLength(2)
                   .IsRequired();

            address.Property(a => a.PostalCode)
                   .HasColumnName("Address_PostalCode")
                   .HasMaxLength(20)
                   .IsRequired();

            address.Property(a => a.City)
                   .HasColumnName("Address_City")
                   .HasMaxLength(100)
                   .IsRequired();

            address.Property(a => a.Street)
                   .HasColumnName("Address_Street")
                   .HasMaxLength(150)
                   .IsRequired();

            address.Property(a => a.BuildingNumber)
                   .HasColumnName("Address_BuildingNumber")
                   .HasMaxLength(20)
                   .IsRequired();

            address.Property(a => a.ApartmentNumber)
                   .HasColumnName("Address_ApartmentNumber")
                   .HasMaxLength(20)
                   .IsRequired(false);
        });

        builder.Property(j => j.Deadline).IsRequired();

        builder.Property(j => j.CreatedAt).IsRequired();

        builder.Property(j => j.UpdatedAt).IsRequired();

        builder.HasMany(j => j.Assignees)
            .WithOne()
            .HasForeignKey(a => a.JobId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(j => j.Assignees)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
