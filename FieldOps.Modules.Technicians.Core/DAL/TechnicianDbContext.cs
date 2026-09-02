using FieldOps.Modules.Technicians.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Technicians.Core.DAL;

internal class TechnicianDbContext(DbContextOptions<TechnicianDbContext> options) : DbContext(options)
{
    public DbSet<Technician> Technicians { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("technicians");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
