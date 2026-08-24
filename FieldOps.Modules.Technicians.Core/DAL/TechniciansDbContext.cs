using FieldOps.Modules.Technicians.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Technicians.Core.DAL;

internal class TechniciansDbContext(DbContextOptions<TechniciansDbContext> options) : DbContext(options)
{
    public DbSet<Technician> Technicians { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("technicians");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
