namespace FieldOps.Modules.Technicians.Contracts;

public interface ITechniciansModuleApi
{
    Task<Guid?> GetTechnicianIdByAccountId (Guid accountId);
}
