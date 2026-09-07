using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.WorkOrders.Core.Exceptions;

internal class WorkOrderImmutableException(Guid id, string currentStatus, string operation)
    : BaseException($"Cannot {operation} work order '{id}' because it is {currentStatus}.");
