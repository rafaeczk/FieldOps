using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Accounts.Core.Events.Foreign.OperatorDeleted
{
    internal record OperatorDeletedEvent(
        Guid AccountId
    ) : IEvent;
}
