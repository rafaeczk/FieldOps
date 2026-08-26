using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.WorkOrders.Api.Controllers;

internal class HomeController : BaseController
{
    [HttpGet]
    public static ActionResult<string> Get() => "WorkOrders API";
}
