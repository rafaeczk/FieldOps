using FieldOps.Modules.Accounts.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Accounts.Core.DAL;

internal class AccountsDbContext(DbContextOptions<AccountsDbContext> options) : DbContext(options)
{
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("users");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
