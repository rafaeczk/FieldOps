using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Accounts.Core.Events.Foreign.TechnicianCreated
{
    internal class TechnicianCreatedEventHandler(IIdentityService identity) : IEventHandler<TechnicianCreatedEvent>
    {
        private readonly IIdentityService identity = identity;

        public async Task HandleAsync(TechnicianCreatedEvent @event)
        {
            await identity.CreateAccountAsync(new(@event.RequestedAccountId, @event.RequestedEmail, @event.RequestedPassword, new(AccountRole.Technician)));
        }
    }
}
