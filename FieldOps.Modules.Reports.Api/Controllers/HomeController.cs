using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Reports.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public static ActionResult<string> Get() => "Reports API";
}
