using FieldOps.Modules.Assets.Core.DTOs;
using FieldOps.Modules.Assets.Core.Services;
using FieldOps.Shared.Infrastructure.Api;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Assets.Api.Controllers;

internal class AssetsController(IAssetService service) : BaseController
{
    private readonly IAssetService service = service;

    [HttpPost]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult<Guid>> CreateAsset([FromBody] CreateAssetDto dto)
    {
        var id = await service.CreateAsync(dto);
        return Ok(id);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AssetDetailsDto>> GetAsset(Guid id)
    {
        return this.OkOrNotFound(await service.GetByAsync(id));
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AssetDto>>> BrowseAssets()
    {
        return Ok(await service.BrowseAsync());
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> UpdateAsset(Guid id, [FromBody] EditAssetDto dto)
    {
        await service.UpdateAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult> DeleteAsset(Guid id)
    {
        await service.DeleteAsync(id);
        return Ok();
    }
}
