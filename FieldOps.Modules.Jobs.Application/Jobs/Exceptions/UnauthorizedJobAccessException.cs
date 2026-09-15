using FieldOps.Shared.Abstractions.Errors;
using System.Net;

namespace FieldOps.Modules.Jobs.Application.Jobs.Exceptions;

public class UnauthorizedJobAccessException : BaseException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;

    public Guid? JobId { get; }

    public UnauthorizedJobAccessException() : base("Unauthorized job access.") 
    {
    }

    public UnauthorizedJobAccessException(Guid jobId) : base($"Unauthorized job access with job id: '{jobId}'.")
    {
        JobId = jobId;
    }
}
