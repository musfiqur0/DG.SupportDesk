using FluentValidation;

namespace DG.SupportDesk.Application.Features.Tenants.Queries.GetTenantByCode;


public sealed class GetTenantByCodeQueryValidator : AbstractValidator<GetTenantByCodeQuery>
{
    public GetTenantByCodeQueryValidator()
    {
        RuleFor(x => x.Code)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("Tenant code is required.")
            .MaximumLength(50)
            .WithMessage("Tenant code cannot exceed 50 characters.")
            .Matches("^[A-Z0-9_-]+$")
            .WithMessage("Tenant code can contain only uppercase letters, numbers, underscore, and hyphen.");
    }
}