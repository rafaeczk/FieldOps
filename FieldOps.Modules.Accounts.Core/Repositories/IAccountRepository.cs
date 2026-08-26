using FieldOps.Modules.Accounts.Core.Entities;

namespace FieldOps.Modules.Accounts.Core.Repositories;

internal interface IAccountRepository
{
    Task<Account?> GetAsync(Guid id);
    Task<Account?> GetAsync(string email);
    Task CreateAsync(Account account);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Account account);
}
