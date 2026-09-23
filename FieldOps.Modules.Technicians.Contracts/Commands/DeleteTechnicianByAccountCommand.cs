using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Technicians.Contracts.Commands;

public record DeleteTechnicianByAccountCommand(Guid AccountId) : IMessage;
