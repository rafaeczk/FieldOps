using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Jobs.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public static ActionResult<string> Get() => "Jobs API";
}
