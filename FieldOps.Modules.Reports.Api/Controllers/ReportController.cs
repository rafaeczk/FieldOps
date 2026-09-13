using FieldOps.Modules.Reports.Api.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Commands;
using FieldOps.Modules.Reports.Application.Reports.DTOs;
using FieldOps.Modules.Reports.Application.Reports.Queries;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Abstractions.Pagination;
using FieldOps.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Reports.Api.Controllers;

internal class ReportsController(IMessageDispatcher messageDispatcher) : BaseController
{
    private readonly IMessageDispatcher messageDispatcher = messageDispatcher;

    [HttpPost]
    [Authorize(Roles = "ADMIN,TECHNICIAN")]
    public async Task<ActionResult<Guid>> CreateAsync(CreateReportDto dto)
    {
        var reportId = await messageDispatcher.Send(new CreateReportCommand(dto.JobId, dto.AssetId, dto.Note, dto.Address, dto.FileIds));
        return Ok(reportId);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "ADMIN,TECHNICIAN")]
    public async Task<ActionResult> EditAsync(Guid id, EditReportCommandDto dto)
    {
        await messageDispatcher.Send(new EditReportCommand(id, dto.Version, dto.Note, dto.Address));
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "ADMIN,TECHNICIAN,OPERATOR")]
    public async Task<ActionResult<ReportDetailsDto>> GetAsync(Guid id)
    {
        return this.OkOrNotFound(await messageDispatcher.Send(new GetReportQuery(id)));
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,TECHNICIAN,OPERATOR")]
    public async Task<ActionResult<PagedResult<ReportListItemDto>>> BrowseAsync([FromQuery] int? pageNumber, [FromQuery] int? pageSize)
    {
        return Ok(await messageDispatcher.Send(new BrowseReportsQuery(new PaginationParams(pageNumber, pageSize))));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN,TECHNICIAN")]
    public async Task<ActionResult> DeleteAsync(Guid id, DeleteReportDto dto)
    {
        await messageDispatcher.Send(new DeleteReportCommand(id, dto.Version));
        return NoContent();
    }

    [HttpPost("{id:guid}/attachments")]
    public async Task<ActionResult> AddAttachment(Guid id, [FromBody] ReportAttachmentActionDto dto)
    {
        await messageDispatcher.Send(new AddReportAttachmentCommand(id, dto.FileId));
        return NoContent();
    }
    [HttpDelete("{id:guid}/attachments")]
    public async Task<ActionResult> RemoveAttachment(Guid id, [FromBody] ReportAttachmentActionDto dto)
    {
        await messageDispatcher.Send(new RemoveReportAttachmentCommand(id, dto.FileId));
        return NoContent();
    }
}
