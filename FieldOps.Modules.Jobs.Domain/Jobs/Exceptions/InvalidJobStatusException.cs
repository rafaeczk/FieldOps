using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;

internal class InvalidJobStatusException : BaseException
{
    public Guid? JobId { get; }
    public string Status { get; }

    public InvalidJobStatusException(Guid jobId, string status) : base($"Invalid job status: '{status}' for job with id: '{jobId}'.")
    {
        JobId = jobId;
        Status = status;
    }

    public InvalidJobStatusException(string status) : base($"Invalid job status: '{status}'.")
    {
        Status = status;
    }
}
