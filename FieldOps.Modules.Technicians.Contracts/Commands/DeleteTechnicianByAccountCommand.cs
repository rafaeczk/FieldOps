using MediatR;

namespace FieldOps.Modules.Technicians.Contracts.Commands;

public record DeleteTechnicianByAccountCommand(Guid AccountId) : IRequest;
