using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;

public class UnauthorizedJobAccessException(Guid jobId)
    : BaseException($"Unauthorized access to job with id: '{jobId}'.")
{
    public Guid JobId => jobId;
}
