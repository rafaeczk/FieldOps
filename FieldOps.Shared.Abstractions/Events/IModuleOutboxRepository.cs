namespace FieldOps.Shared.Abstractions.Events;

public interface IModuleOutboxRepository
{
    Task<List<IOutboxMessageDto>> BrowseUnprocessedAsync(int batchSize);
    Task MarkAsProcessedAsync(Guid id);
}
