using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.WorkOrders.Core.Exceptions;

internal class InvalidWorkOrderStatusException(string status)
    : BaseException($"Work order status '{status}' is not valid.");
