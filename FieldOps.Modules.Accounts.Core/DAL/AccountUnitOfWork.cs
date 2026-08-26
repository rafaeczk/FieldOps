using FieldOps.Modules.Accounts.Core.Repositories;

namespace FieldOps.Modules.Accounts.Core.DAL;

internal class AccountUnitOfWork(AccountDbContext context) : IAccountUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
