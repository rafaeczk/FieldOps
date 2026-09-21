using FieldOps.Modules.Assets.Core.DTOs;
using FluentValidation;

namespace FieldOps.Modules.Assets.Core.Validators;

public sealed class UpdateAssetDtoValidator : AbstractValidator<EditAssetDto>
{

    public UpdateAssetDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Asset name is required.")
            .MaximumLength(100).WithMessage("Asset name cannot exceed 100 characters.");

        RuleFor(x => x.SerialNumber)
            .NotEmpty().WithMessage("Serial number is required.")
            .MaximumLength(50).WithMessage("Serial number cannot exceed 50 characters.");

        RuleFor(x => x.Model)
            .NotEmpty().WithMessage("Model is required.")
            .MaximumLength(50).WithMessage("Model cannot exceed 50 characters.");

        RuleFor(x => x.Manufacturer)
            .NotEmpty().WithMessage("Manufacturer is required.")
            .MaximumLength(50).WithMessage("Manufacturer cannot exceed 50 characters.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.");
           

        RuleFor(x => x.Notes)
            .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters.")
            .When(x => !string.IsNullOrEmpty(x.Notes));


        RuleFor(x => x.PurchaseDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Purchase date cannot be in the future.")
            .When(x => x.PurchaseDate.HasValue);

        RuleFor(x => x.WarrantyExpires)
            .GreaterThan(x => x.PurchaseDate)
            .WithMessage("Warranty expiration date must be after the purchase date.")
            .When(x => x.WarrantyExpires.HasValue && x.PurchaseDate.HasValue);

        RuleFor(x => x.LastServiceDate)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Last service date cannot be in the future.")
            .When(x => x.LastServiceDate.HasValue);

        RuleFor(x => x.LastServiceDate)
            .GreaterThanOrEqualTo(x => x.PurchaseDate)
            .WithMessage("Last service date cannot be earlier than the purchase date.")
            .When(x => x.LastServiceDate.HasValue && x.PurchaseDate.HasValue);
    }
}