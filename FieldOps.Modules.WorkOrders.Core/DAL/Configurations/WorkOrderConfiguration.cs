using FieldOps.Modules.WorkOrders.Core.Entities;
using FieldOps.Modules.WorkOrders.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.WorkOrders.Core.DAL.Configurations;

internal class WorkOrderConfiguration : IEntityTypeConfiguration<WorkOrder>
{
    public void Configure(EntityTypeBuilder<WorkOrder> builder)
    {
        builder.HasKey(wo => wo.Id);

        builder.Property(wo => wo.Title).IsRequired().HasMaxLength(200);

        builder.Property(wo => wo.Address).IsRequired().HasMaxLength(500);

        builder.Property(wo => wo.Status)
            .IsRequired()
            .HasConversion(v => v.Value, v => new WorkOrderStatus(v));

        builder.Property(wo => wo.OperatorId).IsRequired();

        builder.Property(wo => wo.CreatedAt).IsRequired();

        builder.Property(wo => wo.UpdatedAt).IsRequired();

        builder.HasIndex(wo => wo.TechnicianId);

        builder.HasIndex(wo => wo.OperatorId);

        builder.HasIndex(wo => wo.Status);
    }
}
