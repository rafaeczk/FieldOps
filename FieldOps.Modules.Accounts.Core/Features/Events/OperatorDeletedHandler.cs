using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Operators.Contracts.Events;
using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class OperatorDeletedHandler(IIdentityService identity) : IIntegrationEventHandler<OperatorDeleted>
{
    public async Task HandleAsync(OperatorDeleted @event, CancellationToken cancellationToken)
    {
        await identity.DeleteAccountAsync(@event.AccountId);
    }
}
