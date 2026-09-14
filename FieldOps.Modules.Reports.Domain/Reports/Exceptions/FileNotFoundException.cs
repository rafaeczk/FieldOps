using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class FileNotFoundException : BaseException
{
    public FileNotFoundException() : base("File not found.")
    { }

    public FileNotFoundException(Guid fileId) : base($"File with id: '{fileId}' was not found.")
    {
        FileId = fileId;
    }

    public Guid? FileId { get; }
}
