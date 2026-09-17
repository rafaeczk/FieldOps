using FieldOps.Modules.Assets.Core.DTOs;
using FluentValidation;

namespace FieldOps.Modules.Assets.Core.Validation;

public sealed class CreateAssetValidator : AbstractValidator<CreateAssetDto>
{
    public CreateAssetValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage("Serial number is required.")
            .MaximumLength(100).WithMessage("Serial number must not exceed 100 characters.");

        RuleFor(x => x.Model)
            .MaximumLength(200).WithMessage("Model must not exceed 200 characters.")
            .When(x => x.Model is not null);

        RuleFor(x => x.Manufacturer)
            .MaximumLength(200).WithMessage("Manufacturer must not exceed 200 characters.")
            .When(x => x.Manufacturer is not null);

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .Must(s => s.Equals("Active", StringComparison.OrdinalIgnoreCase)
                     || s.Equals("UnderMaintenance", StringComparison.OrdinalIgnoreCase)
                     || s.Equals("InRepair", StringComparison.OrdinalIgnoreCase)
                     || s.Equals("Decommissioned", StringComparison.OrdinalIgnoreCase))
            .WithMessage("Status must be one of: Active, UnderMaintenance, InRepair, Decommissioned.");

        RuleFor(x => x.PurchaseDate)
            .LessThanOrEqualTo(DateTime.UtcNow.AddDays(1))
            .WithMessage("Purchase date cannot be in the far future.")
            .When(x => x.PurchaseDate.HasValue);

        RuleFor(x => x.WarrantyExpires)
            .GreaterThanOrEqualTo(x => x.PurchaseDate ?? DateTime.MinValue)
            .WithMessage("Warranty expiry must be on or after purchase date.")
            .When(x => x.WarrantyExpires.HasValue && x.PurchaseDate.HasValue);

        RuleFor(x => x.Notes)
            .MaximumLength(2000).WithMessage("Notes must not exceed 2000 characters.")
            .When(x => x.Notes is not null);
    }
}
