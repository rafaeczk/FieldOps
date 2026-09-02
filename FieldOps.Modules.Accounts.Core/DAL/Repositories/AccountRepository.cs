using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Modules.Accounts.Core.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Accounts.Core.DAL.Repositories;

internal class AccountRepository(AccountDbContext context) : IAccountRepository
{
    private readonly AccountDbContext context = context;

    public async Task CreateAsync(Account account)
    {
        await context.Accounts.AddAsync(account);
    }

    public async Task DeleteAsync(Account account)
    {
        context.Accounts.Remove(account);
    }

    public async Task<Account?> GetAsync(AccountId id)
    {
        return await context.Accounts.SingleOrDefaultAsync(a => a.Id == id);
    }

    public async Task<Account?> GetAsync(string email)
    {
        return await context.Accounts.SingleOrDefaultAsync(a => a.Email == email);
    }

    public async Task UpdateAsync(Account account)
    {
        context.Accounts.Update(account);
    }
}
