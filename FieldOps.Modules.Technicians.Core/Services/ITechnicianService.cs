using FieldOps.Modules.Technicians.Core.DTOs;

namespace FieldOps.Modules.Technicians.Core.Services
{
    public interface ITechnicianService
    {
        Task<Guid> CreateAsync(CreateTechnicianDto dto);
        Task<TechnicianDto?> GetByAsync(Guid id);
        Task<IReadOnlyList<TechnicianDto>> BrowseAsync();
        Task DeleteAsync(Guid id);
        Task DeleteByAccountIdAsync(Guid accountId);
    }
}
