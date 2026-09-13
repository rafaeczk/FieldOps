using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Technicians.Core.Features.Queries;

public record GetTechnicianIdByAccountIdQuery(AccountId AccountId) : IMessage<TechnicianId?>;

internal class GetTechnicianIdByAccountIdQueryHandler(ITechnicianRepository repository) : IMessageHandler<GetTechnicianIdByAccountIdQuery, TechnicianId?>
{
    public async Task<TechnicianId?> HandleAsync(GetTechnicianIdByAccountIdQuery request, CancellationToken ct)
    {
        var @operator = await repository.GetByAccountIdAsync(request.AccountId);
        return @operator?.Id;
    }
}
