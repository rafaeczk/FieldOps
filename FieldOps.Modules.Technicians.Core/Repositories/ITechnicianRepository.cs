

using FieldOps.Modules.Technicians.Core.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Technicians.Core.Repositories;

internal interface ITechnicianRepository
{
    Task CreateAsync(Technician technician);
    Task<bool> ExistsAsync(TechnicianId id);
    Task<Technician?> GetAsync(TechnicianId id);
    Task<Technician?> GetByAccountIdAsync(Guid accountId);
    Task<IReadOnlyList<Technician>> BrowseAsync();
    Task DeleteAsync(Technician technician);
}
