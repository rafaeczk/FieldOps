using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Auth;

namespace FieldOps.Modules.Accounts.Core.Services;

public interface IIdentityService
{
    Task<AccountDto?> GetAsync(Guid id);
    Task<IReadOnlyList<AccountDto>> GetTechniciansAsync();
    Task<JsonWebToken> SignInAsync(SignInCommand dto);
    Task CreateAccountAsync(CreateAccountCommand dto);
    Task DeleteAccountAsync(Guid id);
    Task<AccountDto?> UpdateProfileAsync(Guid id, UpdateProfileCommand dto);
    Task ChangePasswordAsync(Guid id, ChangePasswordCommand dto);
}

public record SignInCommand(string Email, string Password);

public record CreateAccountCommand(Guid Id, string Email, string Password, string FullName, AccountRole Role);

public record UpdateProfileCommand(string Email, string FullName);

public record ChangePasswordCommand(string CurrentPassword, string NewPassword);
