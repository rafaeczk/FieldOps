using System.ComponentModel.DataAnnotations;

namespace FieldOps.Modules.Accounts.Core.DTOs;

public record UpdateProfileDto(
    [Required] string Email,
    [Required, MaxLength(255)] string FullName);

public record ChangePasswordDto(
    [Required] string CurrentPassword,
    [Required, MinLength(6)] string NewPassword);
