using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Users.Api.Controllers;

internal class AccountController : BaseController
{
    [HttpPost("sign-in")]
    public async Task<ActionResult> SignIn()
    {
        throw new NotImplementedException();
    }

    [HttpPost("sign-up")]
    public async Task<ActionResult> SignUp()
    {
        throw new NotImplementedException();
    }

    [HttpGet("me")]
    public async Task<ActionResult> GetMe()
    {
        throw new NotImplementedException();
    }
}
