using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Jobs.Application.Jobs.Exceptions;

public class TechnicianNotFoundException(Guid technicianId) : BaseException($"Technician with id: '{technicianId}' was not found.")
{
    public Guid TechnicianId { get; } = technicianId;
}
