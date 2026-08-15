using FieldOps.Modules.Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.Accounts.Core.DAL.Configurations;

internal class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasKey(a => a.Id);

        builder.HasIndex(a => a.Email).IsUnique();

        builder.Property(a => a.Hash).IsRequired();

        builder.Property(a => a.Role).IsRequired();
    }
}
