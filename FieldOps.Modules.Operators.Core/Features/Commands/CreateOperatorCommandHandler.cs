using FieldOps.Modules.Operators.Contracts.Commands;
using FieldOps.Modules.Operators.Core.DTOs;
using FieldOps.Modules.Operators.Core.Services;
using MediatR;

namespace FieldOps.Modules.Operators.Core.Features.Commands;

internal class CreateOperatorCommandHandler(IOperatorService service) : IRequestHandler<CreateOperatorCommand, Guid>
{
    public async Task<Guid> Handle(CreateOperatorCommand request, CancellationToken cancellationToken)
    {
        return await service.CreateAsync(new CreateOperatorDto(request.FullName, request.Email, request.Password));
    }
}
