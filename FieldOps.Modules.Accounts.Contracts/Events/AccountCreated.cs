using MediatR;

namespace FieldOps.Modules.Accounts.Contracts.Events;

public record AccountCreated(Guid Id, string Email, string Role) : INotification;
