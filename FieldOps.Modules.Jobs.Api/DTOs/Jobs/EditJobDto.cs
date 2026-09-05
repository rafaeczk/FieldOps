using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Jobs.Api.DTOs.Jobs;

public record EditJobDto(string Title, string? Description, string Priority, Address Address, DateTime Deadline);
