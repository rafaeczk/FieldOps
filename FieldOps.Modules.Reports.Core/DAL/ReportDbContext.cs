using FieldOps.Modules.Reports.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Reports.Core.DAL;

internal class ReportDbContext(DbContextOptions<ReportDbContext> options) : DbContext(options)
{
    public DbSet<Report> Reports { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("reports");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
