using FluentValidation;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Queries.GetTenantConfigurationById;

public sealed class GetTenantConfigurationByIdQueryValidator : AbstractValidator<GetTenantConfigurationByIdQuery>
{
    public GetTenantConfigurationByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Tenant configuration id is required.");
    }
}