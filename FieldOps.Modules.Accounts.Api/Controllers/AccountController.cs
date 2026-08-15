using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Shared.Abstractions.Contexts;
using FieldOps.Shared.Infrastructure.Api;
using FieldOps.Shared.Infrastructure.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Accounts.Api.Controllers;

internal class AccountController(IIdentityService identityService, IContext context, AuthOptions authOptions) : BaseController
{
    private readonly IIdentityService identityService = identityService;
    private readonly IContext context = context;

    [HttpPost("sign-in")]
    public async Task<ActionResult> SignIn([FromBody] SignInDto dto)
    {
        var jwt = await identityService.SignInAsync(dto);

        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = DateTimeOffset.FromUnixTimeMilliseconds(jwt.Expires)
        };

        Response.Cookies.Append(authOptions.Challenge, jwt.AccessToken, cookieOptions);

        return Ok();
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUp([FromBody] SignUpDto dto)
    {
        await identityService.SignUpAsync(dto);
        return NoContent();
    }

    [HttpGet("me")]
    public async Task<ActionResult<AccountDto>> GetMe()
    {
        return this.OkOrNotFound(await identityService.GetAsync(context.Identity.Id));
    }
}
