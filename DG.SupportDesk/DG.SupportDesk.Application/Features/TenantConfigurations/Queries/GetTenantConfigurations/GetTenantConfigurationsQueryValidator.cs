using FluentValidation;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Queries.GetTenantConfigurations;

public sealed class GetTenantConfigurationsQueryValidator : AbstractValidator<GetTenantConfigurationsQuery>
{
    public GetTenantConfigurationsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).When(x => x.PageNumber.HasValue)
            .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).When(x => x.PageSize.HasValue)
            .WithMessage("Page size must be greater than 0.");

        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(100).When(x => x.PageSize.HasValue)
            .WithMessage("Page size cannot exceed 100.");
    }
}