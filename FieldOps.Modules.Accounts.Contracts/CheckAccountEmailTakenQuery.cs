using MediatR;

namespace FieldOps.Modules.Accounts.Contracts;

public record CheckAccountEmailTakenQuery(string Email) : IRequest<bool>;
