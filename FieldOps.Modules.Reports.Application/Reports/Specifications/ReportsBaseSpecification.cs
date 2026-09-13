using FieldOps.Modules.Reports.Application.Reports.Exceptions;
using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;
using FieldOps.Shared.Infrastructure.Specification;

namespace FieldOps.Modules.Reports.Application.Reports.Specifications;

public abstract class ReportsBaseSpecification : BaseSpecification<Report>
{
    public ReportsBaseSpecification(string role, TechnicianId? technicianId)
    {
        switch (role)
        {
            case AccountRole.Admin:
            case AccountRole.Operator:
                break;
            case AccountRole.Technician:
                AddCriteria(r => r.CreatorId.Equals(technicianId));
                break;
            default:
                throw new UnauthorizedReportAccessException();
        }

        AddCriteria(r => !r.IsDeleted);
    }
}
