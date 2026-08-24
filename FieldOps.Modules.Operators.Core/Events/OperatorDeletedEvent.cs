using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Operators.Core.Events
{
    internal record OperatorDeletedEvent(
        Guid AccountId
    ) : IEvent;
}
