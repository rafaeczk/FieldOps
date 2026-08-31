using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Technicians.Contracts.Events;
using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class TechnicianDeletedHandler(IIdentityService identity) : IIntegrationEventHandler<TechnicianDeleted>
{
    public async Task HandleAsync(TechnicianDeleted @event, CancellationToken cancellationToken)
    {
        await identity.DeleteAccountAsync(@event.AccountId);
    }
}
