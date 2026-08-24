using MediatR;

namespace FieldOps.Modules.Operators.Contracts;

public record CheckAccountEmailTaken(string Email) : IRequest<bool>;
