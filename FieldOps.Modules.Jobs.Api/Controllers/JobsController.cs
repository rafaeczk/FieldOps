using MediatR;

namespace FieldOps.Modules.Jobs.Api.Controllers;

internal class JobsController(ISender sender) : BaseController
{
    private readonly ISender sender = sender;
}
