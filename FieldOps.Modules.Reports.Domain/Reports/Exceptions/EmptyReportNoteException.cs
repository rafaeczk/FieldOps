using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class EmptyReportNoteException(Guid reportId) : BaseException($"Empty report note for report with id: {reportId}.")
{
    public Guid ReportId => reportId;
}