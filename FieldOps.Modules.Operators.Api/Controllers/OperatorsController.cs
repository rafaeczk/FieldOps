using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Services;
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
}
