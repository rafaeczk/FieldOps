using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Accounts.Core.Events.Foreign.OperatorCreated;

internal class OperatorCreatedEventHandler(IIdentityService identity) : IEventHandler<OperatorCreatedEvent>
{
    private readonly IIdentityService identity = identity;

    public async Task HandleAsync(OperatorCreatedEvent @event)
    {
        await identity.CreateAccountAsync(new(@event.RequestedAccountId, @event.RequestedEmail, @event.RequestedPassword, new(AccountRole.Operator)));
    }
}
