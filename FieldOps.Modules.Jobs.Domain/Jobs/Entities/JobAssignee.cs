using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Jobs.Domain.Jobs.Entities;

public class JobAssignee
{
    public TechnicianId TechnicianId { get; private set; } = null!;
    public JobId JobId { get; private set; } = null!;

    private JobAssignee() { }

    public static JobAssignee Create(TechnicianId technicianId, JobId jobId)
    {
        return new()
        {
            TechnicianId = technicianId,
            JobId = jobId,
        };
    }
}
