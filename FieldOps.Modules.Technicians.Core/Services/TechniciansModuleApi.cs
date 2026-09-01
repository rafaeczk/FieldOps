using FieldOps.Modules.Technicians.Contracts;
using FieldOps.Modules.Technicians.Core.Features.Queries;
using MediatR;

namespace FieldOps.Modules.Technicians.Core.Services;

internal class TechniciansModuleApi(ISender sender) : ITechniciansModuleApi
{
    private readonly ISender sender = sender;

    public Task<Guid?> GetTechnicianIdByAccountId(Guid accountId)
    {
        return sender.Send(new GetTechnicianIdByAccountId(accountId));
    }
}
