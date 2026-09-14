using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Assets.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public static ActionResult<string> Get() => "Assets API";
}
