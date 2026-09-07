using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.WorkOrders.Core.Exceptions;

internal class InvalidWorkOrderTransitionException(string from, string to)
    : BaseException($"Cannot transition work order from '{from}' to '{to}'.");
