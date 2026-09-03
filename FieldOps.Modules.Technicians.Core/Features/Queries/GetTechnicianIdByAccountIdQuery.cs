using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Technicians.Core.Features.Queries;

public record GetTechnicianIdByAccountIdQuery(Guid AccountId) : IMessage<Guid?>;

internal class GetTechnicianIdByAccountIdQueryHandler(ITechnicianRepository repository) : IMessageHandler<GetTechnicianIdByAccountIdQuery, Guid?>
{
    public async Task<Guid?> HandleAsync(GetTechnicianIdByAccountIdQuery request, CancellationToken ct)
    {
        var @operator = await repository.GetByAccountIdAsync(request.AccountId);
        return @operator?.Id;
    }
}
