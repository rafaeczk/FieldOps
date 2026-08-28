using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Modules.Technicians.Contracts.Events;
using MediatR;

namespace FieldOps.Modules.Accounts.Core.Features.Events;

internal class TechnicianCreatedHandler(IIdentityService identity) : INotificationHandler<TechnicianCreated>
{
    public async Task Handle(TechnicianCreated @event, CancellationToken ct)
    {
        await identity.CreateAccountAsync(new(@event.RequestedAccountId, @event.RequestedEmail, @event.RequestedPassword, @event.FullName, new(AccountRole.Technician)));
    }
}
