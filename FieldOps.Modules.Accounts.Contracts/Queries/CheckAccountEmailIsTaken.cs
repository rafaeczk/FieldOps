using FieldOps.Shared.Abstractions.Messages;

namespace FieldOps.Modules.Accounts.Contracts.Queries;

public record CheckAccountEmailIsTaken(string Email) : IMessage<bool>;
