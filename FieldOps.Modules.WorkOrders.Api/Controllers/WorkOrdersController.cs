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

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> UpdateWorkOrder(Guid id, [FromBody] UpdateWorkOrderDto dto)
    {
        await service.UpdateAsync(id, dto);
        return Ok();
    }

    [HttpGet("operator")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult<WorkOrderListDto>> BrowseByOperator([FromQuery] WorkOrderFilterDto filter)
    {
        return Ok(await service.BrowseByOperatorAsync(context.Identity.Id, filter));
    }

    [HttpGet("technician")]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult<WorkOrderListDto>> BrowseByTechnician([FromQuery] WorkOrderFilterDto filter)
    {
        return Ok(await service.BrowseByTechnicianAsync(context.Identity.Id, filter));
    }

    [HttpGet("all")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<WorkOrderListDto>> BrowseAll([FromQuery] WorkOrderFilterDto filter)
    {
        return Ok(await service.BrowseAllAsync(filter));
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

    [HttpDelete("{id}/assign/{technicianId}")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> UnassignTechnician(Guid id, Guid technicianId)
    {
        await service.UnassignTechnicianAsync(id, technicianId);
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
