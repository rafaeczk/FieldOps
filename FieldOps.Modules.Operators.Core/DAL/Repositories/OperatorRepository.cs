using FieldOps.Modules.Operators.Core.Entities;
using FieldOps.Modules.Operators.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Operators.Core.DAL.Repositories;

internal class OperatorRepository(OperatorDbContext context) : IOperatorRepository
{
    private readonly OperatorDbContext context = context;

    public async Task<IReadOnlyList<Operator>> BrowseAsync()
    {
        return await context.Operators.ToListAsync();
    }

    public async Task CreateAsync(Operator @operator)
    {
        context.Operators.Add(@operator);
    }

    public async Task DeleteAsync(Operator @operator)
    {
        context.Operators.Remove(@operator);
    }
    public async Task<Operator?> GetAsync(Guid id)
    {
        return await context.Operators.SingleOrDefaultAsync(o => o.Id == id);
    }
}
