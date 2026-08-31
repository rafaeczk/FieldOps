using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Modules.Technicians.Contracts.Events;
using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class TechnicianCreatedHandler(IIdentityService identity) : IIntegrationEventHandler<TechnicianCreated>
{
    public async Task HandleAsync(TechnicianCreated @event, CancellationToken ct)
    {
        await identity.CreateAccountAsync(new(@event.RequestedAccountId, @event.RequestedEmail, @event.RequestedPassword, new(AccountRole.Technician)));
    }
}
