namespace FieldOps.Modules.Accounts.Core.DTOs;

public record AccountDto(Guid Id, string Email, string Role, DateTime CreatedAt);
