using FieldOps.Modules.Operators.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Operators.Core.DAL;

internal class OperatorDbContext(DbContextOptions<OperatorDbContext> options) : DbContext(options)
{
    public DbSet<Operator> Operators { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("operators");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
