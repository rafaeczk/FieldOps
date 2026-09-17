using FluentValidation;
using FieldOps.Shared.Abstractions.Errors;
using System.Net;

namespace FieldOps.Shared.Infrastructure.Errors;

internal class ExceptionToResponseMapper : IExceptionToResponseMapper
{
    public ErrorResponse Map(Exception exception)
        => exception switch
        {
            ValidationException e => new ErrorResponse(
                new ErrorList(
                    e.Errors.Select(err => new Error(err.ErrorMessage, err.PropertyName)).ToList()),
                HttpStatusCode.BadRequest),

            BaseException e => new ErrorResponse(
                new ErrorList(
                    new List<Error> { new(e.Message, null) }),
                e.StatusCode),

            ConcurrencyException e => new ErrorResponse(
                new ErrorList(
                    new List<Error> { new("Entity version conflict.", null) }),
                e.StatusCode),

            _ => new ErrorResponse(
                new ErrorList(
                    new List<Error> { new("Internal server error.", null) }),
                HttpStatusCode.InternalServerError)
        };

    private record Error(string Message, string? Path);

    private record ErrorList(List<Error> Errors);
}