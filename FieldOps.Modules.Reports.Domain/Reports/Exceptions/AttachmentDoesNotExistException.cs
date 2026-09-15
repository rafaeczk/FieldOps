using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class AttachmentDoesNotExistException(Guid fileId)
    : BaseException($"Attachment with file id: '{fileId}' does not exist.")
{
    public Guid FileId => fileId;
}
