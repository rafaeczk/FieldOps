using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Accounts.Core.Exceptions;

public class InvalidCredentialsException() : BaseException("Invalid credentials")
{
}
