using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Modules.Technicians.Core.Features.Queries;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Technicians.Core.Services;

internal class TechnicianModuleApi(IMessageDispatcher messageDispatcher) : ITechnicianModuleApi
{
    private readonly IMessageDispatcher messageDispatcher = messageDispatcher;

    public Task<bool> GetTechnicianExists(TechnicianId technicianId)
    {
        return messageDispatcher.Send(new GetTechnicianExistsQuery(technicianId));
    }

    public Task<Guid?> GetTechnicianIdByAccountId(AccountId accountId)
    {
        return messageDispatcher.Send(new GetTechnicianIdByAccountIdQuery(accountId));
    }
}
