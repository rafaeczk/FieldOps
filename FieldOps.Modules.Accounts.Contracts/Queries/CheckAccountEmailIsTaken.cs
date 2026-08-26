using MediatR;

namespace FieldOps.Modules.Accounts.Contracts.Queries;

public record CheckAccountEmailIsTaken(string Email) : IRequest<bool>;
