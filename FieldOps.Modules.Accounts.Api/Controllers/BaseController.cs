using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Accounts.Api.Controllers;

[ApiController]
[Route($"api/{BasePath}/[controller]")]
internal class BaseController : ControllerBase
{
    public const string BasePath = "users-module";
}
