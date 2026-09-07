using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Accounts.Core.Exceptions;

internal class AccountNotFoundException() : BaseException("Account not found")
{
}
