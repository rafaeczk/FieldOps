using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class FileNotFoundInReportException(Guid reportId, Guid fileId)
    : BaseException($"Attachment with file id: '{fileId}' does not exist in report with id: '{reportId}'.")
{
    public Guid ReportId => reportId;
    public Guid FileId => fileId;
}
