using FieldOps.Modules.WorkOrders.Core.DTOs;
using FieldOps.Modules.WorkOrders.Core.Services;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.WorkOrders.Api.Controllers;

internal class WorkOrdersController(IWorkOrderService service, IContext context) : BaseController
{
    private readonly IWorkOrderService service = service;
    private readonly IContext context = context;

    [HttpPost]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult<Guid>> CreateWorkOrder([FromBody] CreateWorkOrderDto dto)
    {
        var workOrderId = await service.CreateAsync(dto, context.Identity.Id);
        return Ok(workOrderId);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<WorkOrderDto>> GetWorkOrder(Guid id)
    {
        return this.OkOrNotFound(await service.GetByAsync(id));
    }

    [HttpGet("operator")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult<IReadOnlyList<WorkOrderDto>>> BrowseByOperator()
    {
        return Ok(await service.BrowseByOperatorAsync(context.Identity.Id));
    }

    [HttpGet("technician")]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult<IReadOnlyList<WorkOrderDto>>> BrowseByTechnician()
    {
        return Ok(await service.BrowseByTechnicianAsync(context.Identity.Id));
    }

    [HttpGet("all")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IReadOnlyList<WorkOrderDto>>> BrowseAll()
    {
        return Ok(await service.BrowseAllAsync());
    }

    [HttpPatch("{id}/status")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] UpdateWorkOrderStatusDto dto)
    {
        await service.UpdateStatusAsync(id, dto);
        return Ok();
    }

    [HttpPost("{id}/assign")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> AssignTechnician(Guid id, [FromBody] AssignTechnicianDto dto)
    {
        await service.AssignTechnicianAsync(id, dto);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> DeleteWorkOrder(Guid id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}
