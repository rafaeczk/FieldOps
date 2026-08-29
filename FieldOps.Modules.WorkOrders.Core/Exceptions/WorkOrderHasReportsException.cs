using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.WorkOrders.Core.Exceptions;

internal class WorkOrderHasReportsException(Guid id)
    : BaseException($"Cannot delete work order '{id}' because it has associated reports.");
