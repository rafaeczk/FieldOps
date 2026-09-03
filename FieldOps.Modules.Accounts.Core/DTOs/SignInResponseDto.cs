namespace FieldOps.Modules.Accounts.Core.DTOs;

public record SignInResponseDto(string AccessToken, SignInUserDto User);

public record SignInUserDto(Guid Id, string Email, string FullName, string Role, DateTime CreatedAt);
