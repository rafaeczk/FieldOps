using FieldOps.Shared.Infrastructure.Events;

namespace FieldOps.Modules.Accounts.Core.Entities;

internal class OutboxMessage : IOutboxMessageDto
{
    public Guid Id { get; set; }
    public string Type { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public DateTime? ProcessedOn { get; set; }
}
