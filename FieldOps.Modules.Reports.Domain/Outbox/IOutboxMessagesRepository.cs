using FieldOps.Shared.Abstractions.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Outbox
{
    public interface IOutboxMessagesRepository : IModuleOutboxRepository
    {
        Task AddAsync<Event>(Event @event)
            where Event : INotification;
        Task AddAsync<Event>(params Event[] events)
            where Event : INotification;
    }
}
