using FieldOps.Shared.Abstractions.Auth;
using FieldOps.Modules.Accounts.Core.DTOs;

namespace FieldOps.Modules.Accounts.Core.Services;

public interface IIdentityService
{
    Task<AccountDto?> GetAsync(Guid id);
    Task<JsonWebToken> SignInAsync(SignInDto dto);
    Task SignUpAsync(SignUpDto dto);
}
