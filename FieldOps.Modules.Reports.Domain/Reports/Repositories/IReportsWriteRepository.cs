using FieldOps.Modules.Reports.Domain.Reports.Entities;

namespace FieldOps.Modules.Reports.Domain.Reports.Repositories
{
    public interface IReportsWriteRepository
    {
        void Add(Report report);
        void Update(Report report);
    }
}
