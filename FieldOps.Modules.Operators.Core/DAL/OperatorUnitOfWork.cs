using FieldOps.Modules.Operators.Core.Repositories;

namespace FieldOps.Modules.Operators.Core.DAL;

internal class OperatorUnitOfWork(OperatorDbContext context) : IOperatorUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
