using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Technicians.Core.Exceptions
{
    internal class TechnicianNotFoundException(Guid id) : BaseException($"Technician with ID {id} not found")
    {
    }
}