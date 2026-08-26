using FieldOps.Modules.WorkOrders.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.WorkOrders.Core.DAL;

internal class WorkOrderDbContext(DbContextOptions<WorkOrderDbContext> options) : DbContext(options)
{
    public DbSet<WorkOrder> WorkOrders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("workorders");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
