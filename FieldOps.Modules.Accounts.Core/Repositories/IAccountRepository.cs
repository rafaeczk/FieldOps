using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Accounts.Core.Repositories;

internal interface IAccountRepository
{
    Task<Account?> GetAsync(AccountId id);
    Task<Account?> GetAsync(string email);
    Task CreateAsync(Account account);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Account account);
}
