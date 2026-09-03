using FieldOps.Modules.WorkOrders.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.WorkOrders.Core.DAL.Configurations;

internal class WorkOrderAssigneeConfiguration : IEntityTypeConfiguration<WorkOrderAssignee>
{
    public void Configure(EntityTypeBuilder<WorkOrderAssignee> builder)
    {
        builder.HasKey(a => new { a.WorkOrderId, a.TechnicianId });

        builder.Property(a => a.AssignedAt).IsRequired();

        builder.HasOne<WorkOrder>()
            .WithMany()
            .HasForeignKey(a => a.WorkOrderId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(a => a.TechnicianId);
    }
}
