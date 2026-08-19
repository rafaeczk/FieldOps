using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Services;
using FieldOps.Shared.Infrastructure.Api;
using FieldOps.Shared.Infrastructure.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Operators.Api.Controllers;

internal class OperatorsController(IOperatorService service) : BaseController
{
    private readonly IOperatorService service = service;

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateOperator([FromBody] CreateOperatorDto dto)
    {
        var operatorId = await service.CreateAsync(dto);
        return Ok(operatorId);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OperatorDetalisDto>> GetOperator(Guid id)
    {
        return this.OkOrNotFound(await service.GetByAsync(id));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<OperatorDto>>> BrowseOperators()
    {
        return Ok(await service.BrowseAsync());
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOperator(Guid id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}
