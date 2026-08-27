using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Core.Exceptions;

internal sealed class InvalidSyncStatusException(string status)
    : BaseException($"Sync status '{status}' is not valid.");
