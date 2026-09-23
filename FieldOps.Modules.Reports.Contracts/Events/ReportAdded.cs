using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Reports.Contracts.Events;

public record ReportAdded(Guid Id, Guid CreatorId) : IIntegrationEvent;
