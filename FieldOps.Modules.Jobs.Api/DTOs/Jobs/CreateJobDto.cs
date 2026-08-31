using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;

namespace FieldOps.Modules.Jobs.Api.DTOs.Jobs;

public record CreateJobDto(string Title, string? Description, string Priority, Address Address, DateTime Deadline);
