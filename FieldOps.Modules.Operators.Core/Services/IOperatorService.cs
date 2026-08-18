using FieldOps.Modules.Operators.Core.DTOs;

namespace FieldOps.Modules.Operators.Core.Services;

public interface IOperatorService
{
    Task<Guid> CreateAsync(CreateOperatorDto dto);
}
