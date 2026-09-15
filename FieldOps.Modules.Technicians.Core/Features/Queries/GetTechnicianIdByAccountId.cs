using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Technicians.Core.Features.Queries;

public record GetTechnicianIdByAccountId(Guid AccountId) : IMessage<Guid?>;

internal class GetTechnicianIdByAccountIdHandler(ITechnicianRepository repository) : IMessageHandler<GetTechnicianIdByAccountId, Guid?>
{
    public async Task<Guid?> HandleAsync(GetTechnicianIdByAccountId request, CancellationToken ct)
    {
        var technician = await repository.GetByAccountIdAsync(request.AccountId);
        return technician?.Id;
    }
}
