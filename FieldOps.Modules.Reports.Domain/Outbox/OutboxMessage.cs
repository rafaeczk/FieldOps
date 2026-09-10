using FieldOps.Shared.Abstractions.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Outbox
{
    public class OutboxMessage : IOutboxMessageDto
    {
        public Guid Id { get; set; }
        public string Type { get; set; } = null!;
        public string Content { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? ProcessedOn { get; set; }
    }
}
