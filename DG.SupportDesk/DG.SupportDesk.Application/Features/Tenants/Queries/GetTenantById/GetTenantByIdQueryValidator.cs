using FluentValidation;

namespace DG.SupportDesk.Application.Features.Tenants.Queries.GetTenantById;

public sealed class GetTenantByIdQueryValidator : AbstractValidator<GetTenantByIdQuery>
{
    public GetTenantByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Tenant id is required.");
    }
}