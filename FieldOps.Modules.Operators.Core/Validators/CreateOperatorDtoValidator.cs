using FieldOps.Modules.Operators.Core.DTOs;
using FluentValidation;

namespace FieldOps.Modules.Operators.Core.Validators;

public sealed class CreateOperatorDtoValidator : AbstractValidator<CreateOperatorDto>
{
    public CreateOperatorDtoValidator()
    {
        RuleFor(x => x.RequestedEmail)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.FullName)
            .NotEmpty().WithMessage("Full name is required.");

        RuleFor(x => x.RequestedPassword)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
            .MaximumLength(100).WithMessage("Password cannot exceed 100 characters.")
            .Matches(@"[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
            .Matches(@"[a-z]").WithMessage("Password must contain at least one lowercase letter.")
            .Matches(@"[0-9]").WithMessage("Password must contain at least one number.")
            .Matches(@"[!?*.\-@#$%^&+=_]").WithMessage("Password must contain at least one special character (!?*.@#$%^&+=_-).");
    }
}