using FieldOps.Modules.Reports.Domain.Reports.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Reports.Infrastructure;

internal class ReportDbContext(DbContextOptions<ReportDbContext> options) : DbContext(options)
{
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("reports");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
