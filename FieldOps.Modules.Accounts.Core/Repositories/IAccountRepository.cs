using FieldOps.Modules.Accounts.Core.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Accounts.Core.Repositories;

internal interface IAccountRepository
{
    Task<Account?> GetAsync(AccountId id);
    Task<Account?> GetAsync(string email);
    Task<IReadOnlyList<Account>> GetAllAsync();
    Task<IReadOnlyList<Account>> GetByRoleAsync(AccountRole role);
    Task CreateAsync(Account account);
    Task UpdateAsync(Account account);
    Task DeleteAsync(Account account);
}
