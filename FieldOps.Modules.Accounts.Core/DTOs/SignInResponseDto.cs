namespace FieldOps.Modules.Accounts.Core.DTOs;

public record SignInResponseDto(string AccessToken, SignInUserDto User);

public record SignInUserDto(string Id, string Email, string Role);
