using FieldOps.Shared.Abstractions.Events;
using MediatR;

namespace FieldOps.Modules.Technicians.Core.Repositories;

internal interface IOutboxMessagesRepository : IModuleOutboxRepository
{
    Task CreateAsync<Event>(Event @event)
        where Event : INotification;
}
