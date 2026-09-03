using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Accounts.Core.Exceptions;

public class InvalidPasswordException(string message) : BaseException(message)
{
}
