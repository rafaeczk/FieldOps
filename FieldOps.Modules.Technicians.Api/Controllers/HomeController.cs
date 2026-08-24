using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Technicians.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public static ActionResult<string> Get() => "Technicians API";
}
