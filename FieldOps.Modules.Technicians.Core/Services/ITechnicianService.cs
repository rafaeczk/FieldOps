using FieldOps.Modules.Technicians.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Technicians.Core.Services
{
    public interface ITechnicianService
    {
        Task<Guid> CreateAsync(CreateTechnicianDto dto);
        Task<TechnicianDto?> GetByAsync(Guid id);
        Task<IReadOnlyList<TechnicianDto>> BrowseAsync();
        Task DeleteAsync(Guid id);
    }
}
