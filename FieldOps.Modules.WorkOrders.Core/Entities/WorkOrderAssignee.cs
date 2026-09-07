namespace FieldOps.Modules.WorkOrders.Core.Entities;

internal class WorkOrderAssignee
{
    public Guid WorkOrderId { get; private set; }
    public Guid TechnicianId { get; private set; }
    public DateTime AssignedAt { get; private set; }

    private WorkOrderAssignee() { }

    public static WorkOrderAssignee Create(Guid workOrderId, Guid technicianId, DateTime assignedAt)
    {
        return new WorkOrderAssignee
        {
            WorkOrderId = workOrderId,
            TechnicianId = technicianId,
            AssignedAt = assignedAt
        };
    }
}
