namespace FieldOps.Shared.Infrastructure.Events;

public interface IModuleOutboxRepository
{
    Task<List<IOutboxMessageDto>> BrowseUnprocessedAsync(int batchSize);
    Task MarkAsProcessedAsync(Guid id);
}
