using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Technicians.Contracts;

public interface ITechnicianModuleApi
{
    Task<Guid?> GetTechnicianIdByAccountId(AccountId accountId);
    Task<bool> GetTechnicianExists(TechnicianId technicianId);
}
