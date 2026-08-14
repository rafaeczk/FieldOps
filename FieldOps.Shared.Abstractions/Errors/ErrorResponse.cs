using System.Net;

namespace FieldOps.Shared.Abstractions.Errors;

public record ErrorResponse(object Response, HttpStatusCode StatusCode);
