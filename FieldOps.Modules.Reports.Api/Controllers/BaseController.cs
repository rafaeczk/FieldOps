using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Reports.Api.Controllers;

[ApiController]
[Route($"api/{BasePath}/[controller]")]
internal class BaseController : ControllerBase
{
    public const string BasePath = "reports-module";
}
