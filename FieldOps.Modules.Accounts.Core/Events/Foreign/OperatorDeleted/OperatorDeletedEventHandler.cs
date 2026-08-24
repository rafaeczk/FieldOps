using FieldOps.Modules.Accounts.Core.Events.Foreign.OperatorCreated;
using FieldOps.Modules.Accounts.Core.Services;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Accounts.Core.Events.Foreign.OperatorDeleted
{
    internal class OperatorDeletedEventHandler(IIdentityService identity) : IEventHandler<OperatorDeletedEvent>
    {
        private readonly IIdentityService identity = identity;

        public async Task HandleAsync(OperatorDeletedEvent @event)
        {
            await identity.DeleteAccountAsync(@event.AccountId);
        }
    }
}
