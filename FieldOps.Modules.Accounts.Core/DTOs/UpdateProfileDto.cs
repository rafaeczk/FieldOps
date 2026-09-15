using System.ComponentModel.DataAnnotations;

namespace FieldOps.Modules.Accounts.Core.DTOs;

public record UpdateProfileDto([Required] string Email);

public record ChangePasswordDto(
    [Required] string CurrentPassword,
    [Required, MinLength(6)] string NewPassword);
