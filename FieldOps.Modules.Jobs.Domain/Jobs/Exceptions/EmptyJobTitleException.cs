using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;

public class EmptyJobTitleException(Guid jobId) : BaseException($"Empty job title for job with id: '{jobId}'.")
{
    public Guid JobId { get; } = jobId;
}
