using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Operators.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public static ActionResult<string> Get() => "Operators API";
}
