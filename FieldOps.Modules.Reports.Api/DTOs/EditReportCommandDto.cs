using FieldOps.Shared.Abstractions.Kernel.ValueObjects;

namespace FieldOps.Modules.Reports.Api.DTOs;

public record EditReportCommandDto(string Note, Address Address);
