using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Operators.Contracts.Commands;

public record CreateOperatorCommand(
    string FullName,
    string Email,
    string Password) : IMessage<Guid>;
