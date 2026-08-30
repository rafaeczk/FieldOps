using MediatR;

namespace FieldOps.Modules.Operators.Contracts.Commands;

public record CreateOperatorCommand(
    string FullName,
    string Email,
    string Password) : IRequest<Guid>;
