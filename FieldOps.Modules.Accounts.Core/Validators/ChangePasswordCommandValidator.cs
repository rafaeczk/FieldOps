using FieldOps.Modules.Accounts.Core.DTOs;
using FieldOps.Modules.Accounts.Core.Services;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Accounts.Core.Validators
{
    public sealed class ChangePasswordCommandValidator : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordCommandValidator()
        {
            RuleFor(x => x.CurrentPassword)
                .NotEmpty().WithMessage("Current password is required.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("New password is required.")
                .MinimumLength(8).WithMessage("New password must be at least 8 characters long.")
                .MaximumLength(100).WithMessage("New password cannot exceed 100 characters.")
                .Matches(@"[A-Z]").WithMessage("New password must contain at least one uppercase letter.")
                .Matches(@"[a-z]").WithMessage("New password must contain at least one lowercase letter.")
                .Matches(@"[0-9]").WithMessage("New password must contain at least one number.")
                .Matches(@"[!?*.@#$%^&+=_-]").WithMessage("New password must contain at least one special character (!?*.@#$%^&+=_-).");
        }
    }
}
