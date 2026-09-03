using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Exceptions;

public class AssigneeAlreadyExists(Guid jobId, Guid technicianId)
    : BaseException($"Assignee with technician id: '{technicianId}' already exists in job with id: '{jobId}'.")
{
    public Guid JobId => jobId;
    public Guid TechnicianId => technicianId;
}
