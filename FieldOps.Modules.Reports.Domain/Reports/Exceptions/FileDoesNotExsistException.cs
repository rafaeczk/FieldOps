using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class FileDoesNotExistException() : BaseException($"One or more files do not exist.")
{
}
