using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.WorkOrders.Api.Controllers;

[ApiController]
[Route($"api/{BasePath}/[controller]")]
internal class BaseController : ControllerBase
{
    public const string BasePath = "workorders-module";
}
