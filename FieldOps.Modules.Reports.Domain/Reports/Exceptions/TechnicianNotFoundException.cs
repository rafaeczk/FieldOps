using FieldOps.Shared.Abstractions.Errors;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions;

public class TechnicianNotFoundException(Guid accountId) : BaseException($"Technician for account '{accountId}' was not found.")
{
    public Guid AccountId => accountId;
}
