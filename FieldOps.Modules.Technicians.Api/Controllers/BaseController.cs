using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Technicians.Api.Controllers;

[ApiController]
[Route($"api/{BasePath}/[controller]")]
internal class BaseController : ControllerBase
{
    public const string BasePath = "technicians-module";
}
