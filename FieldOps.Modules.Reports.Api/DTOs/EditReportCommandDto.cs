using FieldOps.Modules.Reports.Domain.Reports.ValueObjects;

namespace FieldOps.Modules.Reports.Api.DTOs;

public record EditReportCommandDto(string Note, Address Address);
