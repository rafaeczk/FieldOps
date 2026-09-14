using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Assets.Api.Controllers;

[ApiController]
[Route($"api/{BasePath}/[controller]")]
internal class BaseController : ControllerBase
{
    public const string BasePath = "assets-module";
}
