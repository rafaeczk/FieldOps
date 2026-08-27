using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Files.Core.Exceptions;

internal class StoredFileNotFoundException(Guid fileId) : BaseException($"File not found for id: ${fileId}.")
{ }
