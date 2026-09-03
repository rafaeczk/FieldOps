using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Configurations;

internal class JobAssigneeConfiguration : IEntityTypeConfiguration<JobAssignee>
{
    public void Configure(EntityTypeBuilder<JobAssignee> builder)
    {
        builder.ToTable("JobAssignees");

        builder.HasKey(a => new { a.JobId, a.TechnicianId });

        builder.Property(a => a.JobId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new(x));

        builder.Property(a => a.TechnicianId)
            .IsRequired()
            .HasConversion(x => x.Value, x => new(x));
    }
}
