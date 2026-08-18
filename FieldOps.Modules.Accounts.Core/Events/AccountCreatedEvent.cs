using FieldOps.Shared.Abstractions.Events;

namespace FieldOps.Modules.Accounts.Core.Events;

internal record AccountCreatedEvent(Guid Id, string Email, string Role) : IEvent;
