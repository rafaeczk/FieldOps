using FieldOps.Modules.Technicians.Core.DTOs;
using FieldOps.Modules.Technicians.Core.Services;
using FieldOps.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Technicians.Api.Controllers;

internal class TechniciansController(ITechnicianService service) : BaseController
{
    private readonly ITechnicianService service = service;

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<Guid>> CreateTechnician([FromBody] CreateTechnicianDto dto)
    {
        var technicianId = await service.CreateAsync(dto);
        return Ok(technicianId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TechnicianDto>> GetTechnician(Guid id)
    {
        return this.OkOrNotFound(await service.GetByAsync(id));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<TechnicianDto>>> BrowseTechnicians()
    {
        return Ok(await service.BrowseAsync());
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult> DeleteTechnician(Guid id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}
