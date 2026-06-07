using FluentValidation;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.HardDeleteTenantConfiguration;

public sealed class HardDeleteTenantConfigurationCommandValidator : AbstractValidator<HardDeleteTenantConfigurationCommand>
{
    public HardDeleteTenantConfigurationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Tenant configuration id is required.");

        RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant id is required.");
    }
}