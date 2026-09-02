using FieldOps.Modules.Technicians.Core.DTOs;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Technicians.Core.Services
{
    public interface ITechnicianService
    {
        Task<Guid> CreateAsync(CreateTechnicianDto dto);
        Task<TechnicianDto?> GetByAsync(TechnicianId id);
        Task<IReadOnlyList<TechnicianDto>> BrowseAsync();
        Task DeleteAsync(TechnicianId id);
    }
}
