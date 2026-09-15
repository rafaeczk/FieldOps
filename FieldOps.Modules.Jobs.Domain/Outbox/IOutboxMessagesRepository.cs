using FieldOps.Shared.Abstractions.Events;
using MediatR;

namespace FieldOps.Modules.Jobs.Domain.Outbox;

public interface IOutboxMessagesRepository : IModuleOutboxRepository
{
    Task AddAsync<Event>(Event @event)
        where Event : INotification;
    Task AddAsync<Event>(params Event[] events)
        where Event : INotification;
}
