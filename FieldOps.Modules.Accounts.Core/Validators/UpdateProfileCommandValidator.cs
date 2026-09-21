using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.Services;
using FluentValidation;

namespace FieldOps.Modules.Accounts.Core.Validators;

public sealed class UpdateProfileCommandValidator : AbstractValidator<UpdateProfileCommand>
{
    public UpdateProfileCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}