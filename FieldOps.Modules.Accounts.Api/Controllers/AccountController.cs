using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Infrastructure.Api;
using FieldOps.Shared.Infrastructure.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Accounts.Api.Controllers;

internal class AccountController(IIdentityService identityService, IContext context, AuthOptions authOptions) : BaseController
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
            new UpdateProfileCommand(dto.Email, dto.FullName));

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
}
