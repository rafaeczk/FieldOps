using System.Net;

namespace FieldOps.Shared.Abstractions.Errors;

public sealed class ConcurrencyException : Exception
{
    public HttpStatusCode StatusCode => HttpStatusCode.Conflict;
}
