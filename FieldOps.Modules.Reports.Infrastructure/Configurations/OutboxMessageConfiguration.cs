using FieldOps.Modules.Reports.Domain.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.Reports.Infrastructure.Configurations;

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
