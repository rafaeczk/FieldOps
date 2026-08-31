using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Jobs.Application.Jobs.Exceptions;

public class JobNotFoundException(Guid jobId) : BaseException($"Job with id: {jobId} was not found.")
{
    public Guid JobId { get; } = jobId;
}
