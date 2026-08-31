using FieldOps.Modules.Jobs.Api.DTOs.Jobs;
using FieldOps.Modules.Jobs.Application.Jobs.Commands;
using FieldOps.Modules.Jobs.Application.Jobs.DTOs;
using FieldOps.Modules.Jobs.Application.Jobs.Queries;
using FieldOps.Shared.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Jobs.Api.Controllers;

internal class JobsController(ISender sender) : BaseController
{
    private readonly ISender sender = sender;

    [HttpPost]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult<Guid>> CreateAsync(CreateJobDto dto)
    {
        var jobId = await sender.Send(new CreateJobCommand(dto.Title, dto.Description, new(dto.Priority), dto.Address, dto.Deadline));
        return Ok(jobId);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> EditAsync(Guid id, EditJobDto dto)
    {
        await sender.Send(new EditJobCommand(id, dto.Title, dto.Description, new(dto.Priority), dto.Address, dto.Deadline));
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "ADMIN,OPERATOR,TECHNICIAN")]
    public async Task<ActionResult<JobDto>> GetAsync(Guid id)
    {
        return this.OkOrNotFound(await sender.Send(new GetJobQuery(id)));
    }

    [HttpGet]
    [Authorize(Roles = "ADMIN,OPERATOR,TECHNICIAN")]
    public async Task<ActionResult<JobDto>> BrowseAsync(
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize)
    {
        return Ok(await sender.Send(new BrowseJobsQuery(new(pageNumber, pageSize))));
    }
}
