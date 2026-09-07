using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Technicians.Contracts.Commands;

public record CreateTechnicianCommand(
    string FullName,
    string Email,
    string Password) : IMessage<Guid>;
