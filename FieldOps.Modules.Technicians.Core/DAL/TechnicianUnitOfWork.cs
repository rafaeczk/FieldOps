using FieldOps.Modules.Technicians.Core.Repositories;

namespace FieldOps.Modules.Technicians.Core.DAL;

internal class TechnicianUnitOfWork(TechnicianDbContext context) : ITechnicianUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken ct = default)
    {
        return context.SaveChangesAsync(ct);
    }
}
