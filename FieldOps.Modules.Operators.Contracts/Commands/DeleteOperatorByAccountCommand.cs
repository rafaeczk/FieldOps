using MediatR;

namespace FieldOps.Modules.Operators.Contracts.Commands;

public record DeleteOperatorByAccountCommand(Guid AccountId) : IRequest;
