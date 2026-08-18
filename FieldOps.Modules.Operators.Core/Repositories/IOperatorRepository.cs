
using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Entities;

namespace FieldOps.Modules.Operators.Core.Repositories;

internal interface IOperatorRepository
{
    Task<Operator> CreateAsync(CreateOperatorDto dto);
}
