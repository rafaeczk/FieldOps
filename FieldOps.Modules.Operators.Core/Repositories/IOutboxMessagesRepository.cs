using FieldOps.Shared.Abstractions.Events;
using MediatR;

namespace FieldOps.Modules.Operators.Core.Repositories;

internal interface IOutboxMessagesRepository : IModuleOutboxRepository
{
    Task AddAsync<Event>(Event @event)
        where Event : INotification;
}
