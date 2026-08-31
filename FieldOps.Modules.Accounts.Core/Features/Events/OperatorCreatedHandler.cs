using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Modules.Operators.Contracts.Events;
using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class OperatorCreatedHandler(IIdentityService identity) : IIntegrationEventHandler<OperatorCreated>
{
    public async Task HandleAsync(OperatorCreated @event, CancellationToken ct)
    {
        await identity.CreateAccountAsync(new(@event.RequestedAccountId, @event.RequestedEmail, @event.RequestedPassword, new(AccountRole.Operator)));
    }
}
