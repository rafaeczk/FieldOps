using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Accounts.Core.Exceptions;

public class EmailInUseException() : BaseException("Email in use")
{
}
