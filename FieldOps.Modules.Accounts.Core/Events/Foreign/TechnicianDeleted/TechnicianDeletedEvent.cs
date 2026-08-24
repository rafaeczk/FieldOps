using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Accounts.Core.Events.Foreign.TechnicianDeleted
{
    internal record TechnicianDeletedEvent(
        Guid AccountId
    ) : IEvent;
}
