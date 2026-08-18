using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Entities;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Modules.Operators.Core.DAL.Repositories;

internal class OperatorRepository(OperatorDbContext context, IClock clock) : IOperatorRepository
{
    private readonly OperatorDbContext context = context;
    private readonly IClock clock = clock;

    public async Task<Operator> CreateAsync(CreateOperatorDto dto)
    {
        var @operator = Operator.Create(
            Guid.NewGuid(),
            dto.FullName,
            clock.UtcNow());

        context.Operators.Add(@operator);

        await context.SaveChangesAsync();

        return @operator;
    }
}
