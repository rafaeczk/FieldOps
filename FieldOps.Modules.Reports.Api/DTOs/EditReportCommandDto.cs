using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Reports.Api.DTOs;

public record EditReportCommandDto(int Version, string Note, Address Address);
