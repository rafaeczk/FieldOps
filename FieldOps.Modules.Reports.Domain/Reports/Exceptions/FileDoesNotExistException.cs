using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class FileDoesNotExistException( Guid? fileId)
    : BaseException($"File with id: '{fileId}' does not exist.")
{
    public FileDoesNotExistException()
        : this(null) { }

    public Guid? FileId => fileId;
}
