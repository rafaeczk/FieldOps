using MediatR;

namespace FieldOps.Modules.Reports.Api.Controllers;

internal class ReportController(ISender sender) : BaseController
{
    private readonly ISender sender = sender;
}
