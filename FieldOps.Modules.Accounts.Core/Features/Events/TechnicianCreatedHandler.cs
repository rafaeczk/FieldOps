using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Technicians.Contracts.Events;
using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class TechnicianCreatedHandler(IIdentityService identity) : IIntegrationEventHandler<TechnicianCreated>
{
    public async Task HandleAsync(TechnicianCreated @event, CancellationToken ct)
    {
        await identity.CreateAccountAsync(new(@event.RequestedAccountId, @event.RequestedEmail, @event.FullName, @event.RequestedPassword, new(AccountRole.Technician)));
    }
}
