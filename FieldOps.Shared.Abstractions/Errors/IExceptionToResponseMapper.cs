namespace FieldOps.Shared.Abstractions.Errors;

public interface IExceptionToResponseMapper
{
    public ErrorResponse Map(Exception exception);
}
