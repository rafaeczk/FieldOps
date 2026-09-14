using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Operators.Contracts.Commands;

public record DeleteOperatorByAccountCommand(Guid AccountId) : IMessage;
