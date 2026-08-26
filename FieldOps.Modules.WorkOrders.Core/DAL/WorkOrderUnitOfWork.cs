using FieldOps.Modules.WorkOrders.Core.Repositories;
using FieldOps.Shared.Infrastructure.Postgres;

namespace FieldOps.Modules.WorkOrders.Core.DAL;

internal class WorkOrderUnitOfWork(WorkOrderDbContext context) : IWorkOrderUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
