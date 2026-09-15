using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class ReportNotFoundException(Guid reportId) : BaseException($"Report with ID '{reportId}' was not found.")
{
    public Guid ReportId => reportId;
}
