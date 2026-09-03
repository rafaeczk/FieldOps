using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Operators.Core.Services;

public interface IOperatorService
{
    Task<Guid> CreateAsync(CreateOperatorDto dto);
    Task<OperatorDetalisDto?> GetByAsync(OperatorId id);
    Task<IReadOnlyList<OperatorDto>> BrowseAsync();
    Task DeleteAsync(OperatorId id);
    Task DeleteByAccountIdAsync(AccountId accountId);
}
