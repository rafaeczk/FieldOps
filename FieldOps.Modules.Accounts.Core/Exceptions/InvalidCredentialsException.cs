using FieldOps.Shared.Abstractions.Errors;
using System.Net;

namespace FieldOps.Modules.Accounts.Core.Exceptions;

public class InvalidCredentialsException() : BaseException("Invalid credentials")
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
}
