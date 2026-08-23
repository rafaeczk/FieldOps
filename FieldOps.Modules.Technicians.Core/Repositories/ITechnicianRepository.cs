

using FieldOps.Modules.Technicians.Core.Entities;

namespace FieldOps.Modules.Technicians.Core.Repositories;

internal interface ITechnicianRepository
{
    Task CreateAsync(Technician technician);
    Task<Technician?> GetAsync(Guid id);
    Task<IReadOnlyList<Technician>> BrowseAsync();
    Task DeleteAsync(Technician technician);
}
