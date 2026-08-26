using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Operators.Contracts.Events;
using MediatR;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class OperatorDeletedHandler(IIdentityService identity) : INotificationHandler<OperatorDeleted>
{
    public async Task Handle(OperatorDeleted @event, CancellationToken cancellationToken)
    {
        await identity.DeleteAccountAsync(@event.AccountId);
    }
}
