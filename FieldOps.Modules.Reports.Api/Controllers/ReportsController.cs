using FieldOps.Modules.Reports.Core.DTOs;
using FieldOps.Modules.Reports.Core.Services;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Reports.Api.Controllers;

internal class ReportsController(IReportService service, IContext context) : BaseController
{
    private readonly IReportService service = service;
    private readonly IContext context = context;

    [HttpPost]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult<Guid>> CreateReport([FromBody] CreateReportDto dto)
    {
        var reportId = await service.CreateAsync(dto, context.Identity.Id);
        return Ok(reportId);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ReportDto>> GetReport(Guid id)
    {
        return this.OkOrNotFound(await service.GetByAsync(id));
    }

    [HttpGet("workorder/{workOrderId}")]
    [Authorize]
    public async Task<ActionResult<IReadOnlyList<ReportDto>>> BrowseByWorkOrder(Guid workOrderId)
    {
        return Ok(await service.BrowseByWorkOrderAsync(workOrderId));
    }

    [HttpGet("technician")]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult<IReadOnlyList<ReportDto>>> BrowseByTechnician()
    {
        return Ok(await service.BrowseByTechnicianAsync(context.Identity.Id));
    }

    [HttpGet("pending-sync")]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult<IReadOnlyList<ReportDto>>> BrowsePendingSync()
    {
        return Ok(await service.BrowsePendingSyncAsync(context.Identity.Id));
    }

    [HttpPost("{id}/photos")]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult> AddPhoto(Guid id, [FromBody] AddPhotoDto dto)
    {
        await service.AddPhotoAsync(id, dto.PhotoPath);
        return Ok();
    }

    [HttpPost("{id}/signature")]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult> SetSignature(Guid id, [FromBody] SetSignatureDto dto)
    {
        await service.SetSignatureAsync(id, dto.SignaturePath);
        return Ok();
    }

    [HttpPatch("{id}/note")]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult> UpdateNote(Guid id, [FromBody] UpdateReportNoteDto dto)
    {
        await service.UpdateNoteAsync(id, dto);
        return Ok();
    }

    [HttpPost("{id}/sync")]
    [Authorize(Roles = "TECHNICIAN")]
    public async Task<ActionResult> MarkSynced(Guid id)
    {
        await service.MarkSyncedAsync(id);
        return Ok();
    }

    [HttpPost("{id}/conflict")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult> MarkConflict(Guid id)
    {
        await service.MarkConflictAsync(id);
        return Ok();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "TECHNICIAN,ADMIN")]
    public async Task<ActionResult> DeleteReport(Guid id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}

public record AddPhotoDto(string PhotoPath);
public record SetSignatureDto(string SignaturePath);
