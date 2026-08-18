namespace FieldOps.Modules.Operators.Core.DTOs;

public record CreateOperatorDto(
    string FullName,
    string RequestedEmail,
    string RequestedPassword);
