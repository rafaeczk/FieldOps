using FieldOps.Modules.Reports.Application.Reports.Commands;
using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Queries;
using FieldOps.Modules.Reports.Api.DTOs;
using FieldOps.Shared.Abstractions.Pagination;
using FieldOps.Shared.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Reports.Api.Controllers;

internal class ReportsController(ISender sender) : BaseController
{
    private readonly ISender sender = sender;

    [HttpPost]
    [Authorize(Roles = "ADMIN,TECHNICIAN")]
    public async Task<ActionResult<Guid>> CreateAsync(CreateReportDto dto)
    {
        var reportId = await sender.Send(new CreateReportCommand(dto.JobId, dto.AssetId, dto.Note, dto.Address, dto.FileIds));
        return CreatedAtAction(nameof(GetAsync), new { id = reportId }, reportId);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "ADMIN,TECHNICIAN")]
    public async Task<ActionResult> EditAsync(Guid id, EditReportCommandDto dto)
    {
        await sender.Send(new EditReportCommand(id, dto.Note, dto.Address));
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "ADMIN,TECHNICIAN,OPERATOR")]
    public async Task<ActionResult<ReportDetailsDto>> GetAsync(Guid id)
    {
        return this.OkOrNotFound(await sender.Send(new GetReportQuery(id)));
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,TECHNICIAN,OPERATOR")]
    public async Task<ActionResult<PagedResult<ReportListItemDto>>> BrowseAsync([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        return Ok(await sender.Send(new BrowseReportsQuery(new PaginationParams(pageNumber, pageSize))));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN,TECHNICIAN")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        await sender.Send(new DeleteReportCommand(id));
        return NoContent();
    }
}
