using FieldOps.Modules.Reports.Application.Reports.Commands;
using FluentValidation;

namespace FieldOps.Modules.Reports.Application.Validators
{
    public sealed class CreateReportCommandValidator : AbstractValidator<CreateReportCommand>
    {
        public CreateReportCommandValidator()
        {
            RuleFor(x => x.JobId)
                .NotEmpty().WithMessage("Job id cannot be empty.");

            RuleFor(x => x.AssetId)
                .NotEmpty().WithMessage("Asset id cannot be empty.");

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

            RuleForEach(x => x.FileIds)
                .NotEmpty().WithMessage("File id cannot be empty.")
                .When(x => x.FileIds is not null);
        }
    }
}
