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

            RuleForEach(x => x.AssetIds)
                .NotEmpty().WithMessage("Asset id cannot be empty.")
                .When(x => x.AssetIds is not null);

            RuleFor(x => x.Note)
                .NotEmpty().WithMessage("Note is required.")
                .MaximumLength(1000).WithMessage("Note cannot exceed 1000 characters.");

            RuleFor(x => x.Address)
                .NotNull().WithMessage("Address is required.");

            RuleForEach(x => x.FileIds)
                .NotEmpty().WithMessage("File id cannot be empty.")
                .When(x => x.FileIds is not null);
        }
    }
}
