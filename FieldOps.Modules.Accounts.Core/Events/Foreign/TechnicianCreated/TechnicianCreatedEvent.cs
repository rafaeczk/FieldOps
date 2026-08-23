using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Accounts.Core.Events.Foreign.TechnicianCreated
{
    internal record TechnicianCreatedEvent(
        Guid Id,
        string FullName,
        DateTime CreatedAt,
        Guid RequestedAccountId,
        string RequestedEmail,
        string RequestedPassword) : IEvent;
}
