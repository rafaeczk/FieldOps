using FieldOps.Modules.Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Accounts.Core.DAL;

internal class AccountDbContext(DbContextOptions<AccountDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<OutboxMessage> OutboxMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("accounts");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
