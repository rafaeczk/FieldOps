using FieldOps.Shared.Abstractions.Errors;
using System.Net;

namespace FieldOps.Shared.Infrastructure.Errors;

internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ErrorResponse Map(Exception exception)
        => exception switch
        {
            BaseException e => new ErrorResponse(
                new ErrorList(
                    new Error(e.Message,
                    null)),
                HttpStatusCode.BadRequest),

            _ => new ErrorResponse(
                new ErrorList(
                    new Error("Internal server error.",
                    null)),
                HttpStatusCode.InternalServerError)
        };

    private record Error(string Message, string? Path);

    private record ErrorList(params List<Error> Errors);
}
