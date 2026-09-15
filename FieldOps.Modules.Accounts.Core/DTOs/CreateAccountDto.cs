using System.ComponentModel.DataAnnotations;

namespace FieldOps.Modules.Accounts.Core.DTOs;

public record CreateAccountDto(
    [Required] string Email,
    [Required, MaxLength(255)] string FullName,
    [Required, MinLength(6)] string Password,
    [Required] string Role);
