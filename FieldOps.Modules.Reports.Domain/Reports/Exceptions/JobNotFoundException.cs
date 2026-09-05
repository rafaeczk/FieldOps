using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class JobNotFoundException(Guid jobId) : BaseException($"Job with ID '{jobId}' was not found.")
{
    public Guid JobId => jobId;
}
