using FieldOps.Shared.Abstractions.Errors;
using FieldOps.Shared.Abstractions.Kernel.Types;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions
{
    [Serializable]
    internal class EmptyReportNoteException(Guid reportId) : BaseException($"Empty report note for report with id: {reportId}.")
    {
        public Guid ReportId { get; } = reportId;
    }
}