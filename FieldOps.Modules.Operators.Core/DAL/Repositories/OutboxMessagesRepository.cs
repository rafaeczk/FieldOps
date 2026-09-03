using FieldOps.Modules.Operators.Core.Repositories;
using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Time;
using FieldOps.Shared.Infrastructure.Modules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FieldOps.Modules.Operators.Core.DAL.Repositories;

internal class OutboxMessagesRepository(OperatorDbContext context, IClock clock, IModuleSerializer serializer) : IOutboxMessagesRepository
{
    private readonly OperatorDbContext context = context;
    private readonly IClock clock = clock;
    private readonly IModuleSerializer serializer = serializer;

    public async Task CreateAsync<Event>(Event @event)
        where Event : INotification
    {
        await context.OutboxMessages.AddAsync(new()
        {
            Id = Guid.NewGuid(),
            Type = typeof(Event).Name,
            Content = Encoding.UTF8.GetString(serializer.Serialize(@event)),
            CreatedAt = clock.UtcNow(),
            ProcessedOn = null
        });
    }

    public async Task<List<IOutboxMessageDto>> BrowseUnprocessedAsync(int batchSize)
    {
        var messages = await context.OutboxMessages
            .Where(m => m.ProcessedOn == null)
            .OrderBy(m => m.CreatedAt)
            .Take(batchSize)
            .ToListAsync();

        return [.. messages.Cast<IOutboxMessageDto>()];
    }

    public async Task MarkAsProcessedAsync(Guid id)
    {
        var message = await context.OutboxMessages.SingleOrDefaultAsync(m => m.Id == id);
        if (message is null) return;
        message.ProcessedOn = clock.UtcNow();
        await context.SaveChangesAsync();
    }
}
