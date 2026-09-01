using FieldOps.Modules.Reports.Application.Reports.Commands;
using FieldOps.Modules.Reports.Application.Reports.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Reports.Api.Controllers;

internal class ReportController(ISender sender) : BaseController
{
    private readonly ISender sender = sender;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync(CreateReportDto dto)
    {
        var reportId = await sender.Send(new CreateReportCommand(dto.JobId, dto.AssetId, dto.Note, dto.Address, dto.FileIds));
        return Ok(reportId);
    }
}
