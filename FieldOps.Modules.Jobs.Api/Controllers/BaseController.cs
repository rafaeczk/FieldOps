using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Jobs.Api.Controllers;

[ApiController]
[Route($"api/{BasePath}/[controller]")]
internal class BaseController : ControllerBase
{
    public const string BasePath = "jobs-module";
}
