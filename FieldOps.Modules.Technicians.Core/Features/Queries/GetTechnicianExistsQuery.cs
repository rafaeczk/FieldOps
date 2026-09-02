using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Technicians.Core.Features.Queries;

public record GetTechnicianExistsQuery(TechnicianId TechnicianId) : IMessage<bool>;

internal sealed class GetTechnicianExistsQueryHandler(ITechnicianRepository repository) : IMessageHandler<GetTechnicianExistsQuery, bool>
{
    public async Task<bool> HandleAsync(GetTechnicianExistsQuery message, CancellationToken ct)
    {
        return await repository.ExistsAsync(message.TechnicianId);
    }
}
