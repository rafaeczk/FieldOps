using FieldOps.Modules.Jobs.Application.Jobs.Exceptions;
using FieldOps.Modules.Jobs.Domain.Jobs.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;
using FieldOps.Shared.Abstractions.Queries;

namespace FieldOps.Modules.Jobs.Application.Jobs.Specifications;

public abstract class JobsBaseSpecification : BaseSpecification<Job>
{
    public JobsBaseSpecification(string role, TechnicianId? technicianId)
    {
        switch (role)
        {
            case AccountRole.Admin:
            case AccountRole.Operator:
                break;
            case AccountRole.Technician:
                AddCriteria(j => j.Assignees.Any(a => a.TechnicianId.Equals(technicianId)));
                break;
            default:
                throw new UnauthorizedJobAccessException();
        }
    }
}
