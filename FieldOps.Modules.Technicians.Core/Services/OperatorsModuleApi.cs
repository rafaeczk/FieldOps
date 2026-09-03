using FieldOps.Modules.Technicians.Core.Features.Queries;
using FieldOps.Modules.Technicians.Contracts;
using MediatR;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Technicians.Core.Services;

internal class TechnicianModuleApi(ISender sender) : ITechnicianModuleApi
{
    private readonly ISender sender = sender;

    public Task<bool> GetTechnicianExists(TechnicianId technicianId)
    {
        return sender.Send(new GetTechnicianExistsQuery(technicianId));
    }

    public Task<Guid?> GetTechnicianIdByAccountId(AccountId accountId)
    {
        return sender.Send(new GetTechnicianIdByAccountIdQuery(accountId));
    }
}
