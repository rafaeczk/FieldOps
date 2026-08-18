using FieldOps.Modules.Operators.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.Operators.Core.DAL.Configurations;

internal class OperatorConfiguration : IEntityTypeConfiguration<Operator>
{
    public void Configure(EntityTypeBuilder<Operator> builder)
    {
        builder.HasKey(o => o.Id);

        builder.HasIndex(o => o.AccountId).IsUnique();

        builder.Property(o => o.FullName).IsRequired();

        builder.Property(a => a.CreatedAt).IsRequired();

        builder.Property(a => a.UpdatedAt).IsRequired();
    }
}
