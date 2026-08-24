using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Technicians.Core.Events
{
    internal record TechnicianDeletedEvent(
        Guid AccountId
    ) : IEvent;
}
