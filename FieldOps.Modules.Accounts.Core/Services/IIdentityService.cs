using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.ValueObjects;
using FieldOps.Shared.Abstractions.Auth;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Accounts.Core.Services;

public interface IIdentityService
{
    Task<AccountDto?> GetAsync(AccountId id);
    Task<IReadOnlyList<AccountDto>> GetAllAsync();
    Task<IReadOnlyList<AccountDto>> GetTechniciansAsync();
    Task<JsonWebToken> SignInAsync(SignInCommand dto);
    Task CreateAccountAsync(CreateAccountCommand dto);
    Task DeleteAccountAsync(AccountId id);
    Task<AccountDto?> UpdateProfileAsync(AccountId id, UpdateProfileCommand dto);
    Task ChangePasswordAsync(AccountId id, ChangePasswordCommand dto);
}

public record SignInCommand(string Email, string Password);

public record CreateAccountCommand(AccountId Id, string Email, string Password, AccountRole Role);

public record UpdateProfileCommand(string Email);

public record ChangePasswordCommand(string CurrentPassword, string NewPassword);
