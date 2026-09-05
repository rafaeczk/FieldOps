using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

internal class EmptyReportNoteException(Guid reportId) : BaseException($"Empty report note for report with id: {reportId}.")
{
    public Guid ReportId => reportId;
}