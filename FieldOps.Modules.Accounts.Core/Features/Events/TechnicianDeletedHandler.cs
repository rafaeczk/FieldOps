using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Technicians.Contracts.Events;
using MediatR;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class TechnicianDeletedHandler(IIdentityService identity) : INotificationHandler<TechnicianDeleted>
{
    public async Task Handle(TechnicianDeleted @event, CancellationToken cancellationToken)
    {
        await identity.DeleteAccountAsync(@event.AccountId);
    }
}
