using FieldOps.Modules.Jobs.Api.DTOs.Jobs;
using FieldOps.Modules.Jobs.Application.Jobs.Commands;
using FieldOps.Modules.Jobs.Application.Jobs.DTOs;
using FieldOps.Modules.Jobs.Application.Jobs.Queries;
using FieldOps.Shared.Abstractions.Messages;
using FieldOps.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Jobs.Api.Controllers;

[Authorize(Roles = "OPERATOR")]
internal class JobsController(IMessageDispatcher messageDispatcher) : BaseController
{
    private readonly IMessageDispatcher messageDispatcher = messageDispatcher;

    [HttpPost]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult<Guid>> CreateAsync(CreateJobDto dto)
    {
        var jobId = await messageDispatcher.Send(new CreateJobCommand(dto.Title, dto.Description, new(dto.Priority), dto.Address, dto.Deadline));
        return Ok(jobId);
    }

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> EditAsync(Guid id, EditJobDto dto)
    {
        await messageDispatcher.Send(new EditJobCommand(id, dto.Version, dto.Title, dto.Description, new(dto.Priority), dto.Address, dto.Deadline));
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<ActionResult<JobDto>> GetAsync(Guid id)
    {
        return this.OkOrNotFound(await messageDispatcher.Send(new GetJobQuery(id)));
    }

    [HttpPost("{id:guid}/assignees")]
    public async Task<ActionResult> AddAssignee(Guid id, [FromBody] JobAssigneeActionDto dto)
    {
        await messageDispatcher.Send(new AddJobAssigneeCommand(id, dto.TechnicianId));
        return NoContent();
    }

    [HttpDelete("{id:guid}/assignees")]
    public async Task<ActionResult> RemoveAssignee(Guid id, [FromBody] JobAssigneeActionDto dto)
    {
        await messageDispatcher.Send(new RemoveJobAssigneeCommand(id, dto.TechnicianId));
        return NoContent();
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<ActionResult<JobDto>> BrowseAsync(
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize)
    {
        return Ok(await messageDispatcher.Send(new BrowseJobsQuery(new(pageNumber, pageSize))));
    }
}
