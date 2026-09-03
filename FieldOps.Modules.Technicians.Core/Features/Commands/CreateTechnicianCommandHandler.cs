using FieldOps.Modules.Technicians.Contracts.Commands;
using FieldOps.Modules.Technicians.Core.DTOs;
using FieldOps.Modules.Technicians.Core.Services;
using MediatR;

namespace FieldOps.Modules.Technicians.Core.Features.Commands;

internal class CreateTechnicianCommandHandler(ITechnicianService service) : IRequestHandler<CreateTechnicianCommand, Guid>
{
    public async Task<Guid> Handle(CreateTechnicianCommand request, CancellationToken cancellationToken)
    {
        return await service.CreateAsync(new CreateTechnicianDto(request.FullName, request.Email, request.Password));
    }
}
