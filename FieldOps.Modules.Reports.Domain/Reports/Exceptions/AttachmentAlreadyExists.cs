using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class AttachmentAlreadyExists(Guid reportId, Guid fileId)
    : BaseException($"Attachment with file id: '{fileId}' already exists in report with id: '{reportId}'.")
{
    public Guid ReportId => reportId;
    public Guid FileId => fileId;
}
