using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Accounts.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public static ActionResult<string> Get() => "Accounts API";
}
