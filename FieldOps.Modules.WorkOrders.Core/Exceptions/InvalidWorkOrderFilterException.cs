using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.WorkOrders.Core.Exceptions;

internal class InvalidWorkOrderFilterException(string value)
    : BaseException($"'{value}' is not a valid filter value.");