
using FieldOps.Modules.Operators.Core.Entities;

namespace FieldOps.Modules.Operators.Core.Repositories;

internal interface IOperatorRepository
{
    Task CreateAsync(Operator @operator);
    Task<Operator?> GetAsync(Guid id);
    Task<Operator?> GetByAccountIdAsync(Guid accountId);
    Task<IReadOnlyList<Operator>> BrowseAsync();
    Task DeleteAsync(Operator @operator);
}
