using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Entities;
using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Operators.Core.DAL.Repositories;

internal class OperatorRepository(OperatorDbContext context, IClock clock) : IOperatorRepository
{
    private readonly OperatorDbContext context = context;
    private readonly IClock clock = clock;

    public async Task<IReadOnlyList<Operator>> BrowseAsync()
    {
        return await context.Operators.ToListAsync();
    }

    public async Task CreateAsync(Operator @operator)
    {
        context.Operators.Add(@operator);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Operator @operator)
    {
        context.Operators.Remove(@operator);
        await context.SaveChangesAsync();
    }
    public async Task<Operator?> GetAsync(Guid id)
    {
        return await context.Operators.SingleOrDefaultAsync(o => o.Id == id);
    }
}
