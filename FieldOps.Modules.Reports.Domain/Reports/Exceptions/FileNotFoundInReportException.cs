using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class FileNotFoundInReportException(Guid? jobId, Guid? fileId)
    : BaseException($"File with id: '{fileId}' does not exist in job with id: '{jobId}'.")
{
    public FileNotFoundInReportException()
        : this(null, null) { }

    public Guid? JobId => jobId;
    public Guid? FileId => fileId;
}
