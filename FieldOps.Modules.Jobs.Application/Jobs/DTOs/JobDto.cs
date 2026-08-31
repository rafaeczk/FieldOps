using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;

namespace FieldOps.Modules.Jobs.Application.Jobs.DTOs;

public record JobDto(Guid Id, string Title, string? Description, string Status, string Priority, Address Address, DateTime Deadline, DateTime CreatedAt, DateTime UpdatedAt);
