using FieldOps.Shared.Abstractions.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Accounts.Core.Exceptions
{
    internal class AccountNotFoundException(Guid id) : BaseException($"Account with ID {id} not found")
    {
    }
}
