using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Shared.Infrastructure.Errors;

internal interface IExceptionCompositionRoot
{
    ErrorResponse Map(Exception exception);
}
