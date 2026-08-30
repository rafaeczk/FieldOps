using FieldOps.Modules.Operators.Contracts.Commands;
using FieldOps.Modules.Operators.Core.Services;
using MediatR;

namespace FieldOps.Modules.Operators.Core.Features.Commands;

internal class DeleteOperatorByAccountCommandHandler(IOperatorService service) : IRequestHandler<DeleteOperatorByAccountCommand>
{
    public async Task Handle(DeleteOperatorByAccountCommand request, CancellationToken cancellationToken)
    {
        await service.DeleteByAccountIdAsync(request.AccountId);
    }
}
