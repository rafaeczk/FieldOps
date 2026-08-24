using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Operators.Core.Exceptions;

public class EmailInUseException() : BaseException("Email in use")
{
}
