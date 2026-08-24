namespace FieldOps.Modules.Technicians.Core.DTOs;

public record CreateTechnicianDto(
    string FullName,
    string RequestedEmail,
    string RequestedPassword);
