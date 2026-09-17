using FieldOps.Modules.Assets.Core.DTOs;
using FieldOps.Modules.Assets.Core.Entities;
using FluentValidation;

namespace FieldOps.Modules.Assets.Core.Validation;

public sealed class EditAssetValidator : AbstractValidator<EditAssetDto>
{
    public EditAssetValidator()
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
            .Must(s => Enum.TryParse<AssetStatus>(s, true, out _))
            .WithMessage("Invalid asset status. Valid values: Active, UnderMaintenance, InRepair, Decommissioned.");

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
