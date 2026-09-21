using FieldOps.Modules.Reports.Application.Reports.Commands;
using FluentValidation;

namespace FieldOps.Modules.Reports.Application.Validators
{
    public sealed class UpdateReportCommandValidator : AbstractValidator<EditReportCommand>
    {
        public UpdateReportCommandValidator()
        {
            RuleFor(x => x.Note)
                .NotEmpty().WithMessage("Note is required.")
                .MaximumLength(1000).WithMessage("Note cannot exceed 1000 characters.");

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
        }
    }
}
