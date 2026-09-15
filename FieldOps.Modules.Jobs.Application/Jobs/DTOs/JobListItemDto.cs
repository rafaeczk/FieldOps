namespace FieldOps.Modules.Jobs.Application.Jobs.DTOs;

public record JobListItemDto(Guid Id, string Title, string Status, string Priority, DateTime Deadline);
