using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Technicians.Core.Exceptions;

public class EmailInUseException() : BaseException("Email in use")
{
}
