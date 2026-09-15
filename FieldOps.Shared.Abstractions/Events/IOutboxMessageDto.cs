namespace FieldOps.Shared.Abstractions.Events;

public interface IOutboxMessageDto
{
    Guid Id { get; }
    string Type { get; }
    string Content { get; }
}
