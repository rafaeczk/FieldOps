namespace FieldOps.Modules.Accounts.Core.DTOs;

public record AccountDto(Guid Id, string Email, string FullName, string Role, DateTime CreatedAt);
