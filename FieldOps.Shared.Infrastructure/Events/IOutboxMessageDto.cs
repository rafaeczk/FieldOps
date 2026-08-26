namespace FieldOps.Shared.Infrastructure.Events;

public interface IOutboxMessageDto
{
    Guid Id { get; }
    string Type { get; }
    string Content { get; }
}
