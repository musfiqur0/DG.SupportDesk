using FluentValidation;

namespace DG.SupportDesk.Application.Features.Tenants.Commands.CreateTenant;

public sealed class CreateTenantCommandValidator : AbstractValidator<CreateTenantCommand>
{
    public CreateTenantCommandValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Tenant name is required.")
            .MaximumLength(200)
            .WithMessage("Tenant name cannot exceed 200 characters.");

        RuleFor(x => x.Code)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Tenant code is required.")
            .MaximumLength(50)
            .WithMessage("Tenant code cannot exceed 50 characters.")
            .Matches("^[A-Z0-9_-]+$")
            .WithMessage("Tenant code can contain only uppercase letters, numbers, underscore, and hyphen.");

        RuleFor(x => x.TenantTypeId)
            .NotEmpty()
            .WithMessage("Tenant type is required.");
    }
}