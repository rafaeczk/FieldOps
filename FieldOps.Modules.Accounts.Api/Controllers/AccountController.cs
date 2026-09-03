using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Operators.Contracts.Commands;
using FieldOps.Modules.Technicians.Contracts.Commands;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Infrastructure.Api;
using FieldOps.Shared.Infrastructure.Auth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Accounts.Api.Controllers;

internal class AccountController(IIdentityService identityService, IContext context, AuthOptions authOptions, ISender sender) : BaseController
{
    private readonly IIdentityService identityService = identityService;
    private readonly IContext context = context;

    [HttpPost("sign-in")]
    public async Task<ActionResult<SignInResponseDto>> SignIn([FromBody] SignInDto dto)
    {
        var jwt = await identityService.SignInAsync(new(dto.Email, dto.Password));

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.FromUnixTimeMilliseconds(jwt.Expires)
        };

        Response.Cookies.Append(authOptions.Challenge, jwt.AccessToken, cookieOptions);

        var response = new SignInResponseDto(
            jwt.AccessToken,
            new SignInUserDto(Guid.Parse(jwt.Id), jwt.Email, jwt.FullName, jwt.Role, jwt.CreatedAt));

        return Ok(response);
    }

    [HttpGet("me")]
    [Authorize]
    public async Task<ActionResult<AccountDto>> GetMe()
    {
        return this.OkOrNotFound(await identityService.GetAsync(context.Identity.Id));
    }

    [HttpGet("technicians")]
    [Authorize(Roles = "ADMIN,OPERATOR")]
    public async Task<ActionResult<IReadOnlyList<AccountDto>>> GetTechnicians()
    {
        return Ok(await identityService.GetTechniciansAsync());
    }

    [HttpPut("me")]
    [Authorize]
    public async Task<ActionResult<AccountDto>> UpdateMe([FromBody] UpdateProfileDto dto)
    {
        var result = await identityService.UpdateProfileAsync(
            context.Identity.Id,
            new UpdateProfileCommand(dto.Email));

        return Ok(result);
    }

    [HttpPut("me/password")]
    [Authorize]
    public async Task<ActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        await identityService.ChangePasswordAsync(
            context.Identity.Id,
            new ChangePasswordCommand(dto.CurrentPassword, dto.NewPassword));

        return Ok();
    }

    [HttpGet("all")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult<IReadOnlyList<AccountDto>>> GetAll()
    {
        return Ok(await identityService.GetAllAsync());
    }

    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult> Create([FromBody] CreateAccountDto dto)
    {
        var role = dto.Role.ToUpperInvariant();

        if (role is not ("OPERATOR" or "TECHNICIAN"))
            return BadRequest(new { errors = new[] { new { message = "Role must be OPERATOR or TECHNICIAN" } } });

        if (role == "TECHNICIAN")
        {
            var technicianId = await sender.Send(new CreateTechnicianCommand(dto.FullName, dto.Email, dto.Password));
            return Ok(new { id = technicianId });
        }

        var operatorId = await sender.Send(new CreateOperatorCommand(dto.FullName, dto.Email, dto.Password));
        return Ok(new { id = operatorId });
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (id == context.Identity.Id)
            return BadRequest(new { errors = new[] { new { message = "Cannot delete your own account" } } });

        var account = await identityService.GetAsync(id);

        if (account is null)
            return NotFound();

        if (account.Role == "TECHNICIAN")
            await sender.Send(new DeleteTechnicianByAccountCommand(id));
        else if (account.Role == "OPERATOR")
            await sender.Send(new DeleteOperatorByAccountCommand(id));
        else
            await identityService.DeleteAccountAsync(id);

        return NoContent();
    }
}
