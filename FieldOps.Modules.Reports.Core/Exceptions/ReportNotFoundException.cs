using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Core.Exceptions;

internal sealed class ReportNotFoundException(Guid id)
    : BaseException($"Report with ID '{id}' was not found.");
