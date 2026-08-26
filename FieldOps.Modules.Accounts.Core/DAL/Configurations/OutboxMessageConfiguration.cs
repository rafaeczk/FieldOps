using FieldOps.Modules.Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.Accounts.Core.DAL.Configurations;

internal class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Type).IsRequired();

        builder.Property(m => m.Content).IsRequired();

        builder.Property(m => m.CreatedAt).IsRequired();
    }
}
