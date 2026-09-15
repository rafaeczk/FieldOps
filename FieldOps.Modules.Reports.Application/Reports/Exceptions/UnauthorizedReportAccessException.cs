using FieldOps.Shared.Abstractions.Errors;
using System.Net;

namespace FieldOps.Modules.Reports.Application.Reports.Exceptions;

public class UnauthorizedReportAccessException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public Guid? ReportId { get; }

    public UnauthorizedReportAccessException() : base("Unauthorized report access.") 
    {
    }

    public UnauthorizedReportAccessException(Guid reportId) : base($"Unauthorized report access with report id: '{reportId}'.")
    {
        ReportId = reportId;
    }
}
