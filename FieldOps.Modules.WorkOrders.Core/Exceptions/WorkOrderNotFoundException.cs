using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.WorkOrders.Core.Exceptions;

internal class WorkOrderNotFoundException(Guid id)
    : BaseException($"Work order with ID '{id}' was not found.");
