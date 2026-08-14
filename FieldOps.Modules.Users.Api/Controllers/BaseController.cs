using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Users.Api.Controllers;

[ApiController]
[Route($"api/{BasePath}/[controller]")]
internal class BaseController : ControllerBase
{
    public const string BasePath = "users-module";
}
