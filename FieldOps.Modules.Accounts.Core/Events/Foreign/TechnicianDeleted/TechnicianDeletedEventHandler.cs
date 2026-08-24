using FieldOps.Modules.Accounts.Core.Events.Foreign.TechnicianDeleted;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Accounts.Core.Events.Foreign.TechnicianDeleted
{
    internal class TechnicianDeletedEventHandler(IIdentityService identity) : IEventHandler<TechnicianDeletedEvent>
    {
        private readonly IIdentityService identity = identity;

        public async Task HandleAsync(TechnicianDeletedEvent @event)
        {
            await identity.DeleteAccountAsync(@event.AccountId);
        }
    }
}
