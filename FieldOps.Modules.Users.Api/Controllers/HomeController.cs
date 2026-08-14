using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Users.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public static ActionResult<string> Get() => "Users API";
}
