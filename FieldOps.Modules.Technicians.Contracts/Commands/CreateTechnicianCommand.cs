using MediatR;

namespace FieldOps.Modules.Technicians.Contracts.Commands;

public record CreateTechnicianCommand(
    string FullName,
    string Email,
    string Password) : IRequest<Guid>;
