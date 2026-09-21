using FieldOps.Modules.Jobs.Application.Jobs.Commands;
using FieldOps.Modules.Jobs.Domain.Jobs.ValueObjects;
using FluentValidation;

namespace FieldOps.Modules.Jobs.Application.Jobs.Validators
{
    public sealed class UpdateJobCommandValidator : AbstractValidator<EditJobCommand>
    {
        public UpdateJobCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(2000).WithMessage("Description cannot exceed 2000 characters.")
                .When(x => !string.IsNullOrWhiteSpace(x.Description));

            RuleFor(x => x.Priority)
                .NotEmpty().WithMessage("Priority is required.")
                .Must(p => JobPriority.AcceptedValues.Contains(p.Value))
                .WithMessage("Invalid priority value.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address is required.");

            When(x => x.Address is not null, () =>
            {
                RuleFor(x => x.Address.CountryCode)
                    .NotEmpty().WithMessage("Country code is required.")
                    .MaximumLength(2).WithMessage("Country code cannot exceed 2 characters.");

                RuleFor(x => x.Address.PostalCode)
                    .NotEmpty().WithMessage("Postal code is required.");

                RuleFor(x => x.Address.City)
                    .NotEmpty().WithMessage("City is required.");

                RuleFor(x => x.Address.Street)
                    .NotEmpty().WithMessage("Street is required.");

                RuleFor(x => x.Address.BuildingNumber)
                    .NotEmpty().WithMessage("Building number is required.");
            });

            RuleFor(x => x.Deadline)
                .NotEmpty().WithMessage("Deadline is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future.");
        }
    }
}
