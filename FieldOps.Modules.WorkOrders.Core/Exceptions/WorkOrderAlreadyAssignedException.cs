using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.WorkOrders.Core.Exceptions;

internal class WorkOrderAlreadyAssignedException(Guid id)
    : BaseException($"Work order with ID '{id}' is already assigned to a technician.");
