using System.Net;

namespace FieldOps.Shared.Abstractions.Errors;

public abstract class BaseException(string message) : Exception(message)
{
    public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
