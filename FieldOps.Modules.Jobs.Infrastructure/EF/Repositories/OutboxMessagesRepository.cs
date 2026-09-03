using FieldOps.Modules.Jobs.Domain.Outbox;
using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Abstractions.Time;
using FieldOps.Shared.Infrastructure.Modules;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Repositories;

internal class OutboxMessagesRepository(JobsDbContext context, IClock clock, IModuleSerializer serializer) : IOutboxMessagesRepository
{
    private readonly JobsDbContext context = context;
    private readonly IClock clock = clock;
    private readonly IModuleSerializer serializer = serializer;

    public async Task AddAsync<Event>(Event @event)
        where Event : INotification
    {
        context.OutboxMessages.Add(new()
        {
            Id = Guid.NewGuid(),
            Type = typeof(Event).Name,
            Content = Encoding.UTF8.GetString(serializer.Serialize(@event)),
            CreatedAt = clock.UtcNow(),
            ProcessedOn = null
        });
    }

    public async Task AddAsync<Event>(params Event[] events)
        where Event : INotification
        => events.ToList().ForEach(async e => await AddAsync(e));

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
