using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Files.Core.Exceptions;

public class EmptyOrMissingFileException() : BaseException("Empty or missing file.")
{
}
