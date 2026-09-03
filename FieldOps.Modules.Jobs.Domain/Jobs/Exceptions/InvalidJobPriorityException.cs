using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;

internal class InvalidJobPriorityException : BaseException
{
    public Guid? JobId { get; }
    public string Priority { get; }

    public InvalidJobPriorityException(Guid jobId, string priority) : base($"Invalid job priority: '{priority}' for job with id: '{jobId}'.")
    {
        JobId = jobId;
        Priority = priority;
    }

    public InvalidJobPriorityException(string priority) : base($"Invalid job priority: '{priority}'.")
    {
        Priority = priority;
    }
}
