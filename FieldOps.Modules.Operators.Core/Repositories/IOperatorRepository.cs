
using FieldOps.Modules.Operators.Core.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Operators.Core.Repositories;

internal interface IOperatorRepository
{
    Task CreateAsync(Operator @operator);
    Task<Operator?> GetAsync(OperatorId id);
    Task<Operator?> GetByAccountIdAsync(Guid accountId);
    Task<IReadOnlyList<Operator>> BrowseAsync();
    Task DeleteAsync(Operator @operator);
}
