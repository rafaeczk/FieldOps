using FieldOps.Modules.Technicians.Contracts.Commands;
using FieldOps.Modules.Technicians.Core.Services;
using MediatR;

namespace FieldOps.Modules.Technicians.Core.Features.Commands;

internal class DeleteTechnicianByAccountCommandHandler(ITechnicianService service) : IRequestHandler<DeleteTechnicianByAccountCommand>
{
    public async Task Handle(DeleteTechnicianByAccountCommand request, CancellationToken cancellationToken)
    {
        await service.DeleteByAccountIdAsync(request.AccountId);
    }
}
