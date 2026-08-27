using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Files.Api.Controllers;

[ApiController]
[Route($"api/{BasePath}/[controller]")]
internal class BaseController : ControllerBase
{
    public const string BasePath = "files-module";
}
