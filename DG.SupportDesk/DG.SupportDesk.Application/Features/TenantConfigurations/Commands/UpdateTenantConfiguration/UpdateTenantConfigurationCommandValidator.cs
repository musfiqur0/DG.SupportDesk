using FluentValidation;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.UpdateTenantConfiguration;

public sealed class UpdateTenantConfigurationCommandValidator : AbstractValidator<UpdateTenantConfigurationCommand>
{
    public UpdateTenantConfigurationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Tenant configuration id is required.");

        RuleFor(x => x.ConfigurationType)
            .Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Configuration type is required.")
            .MaximumLength(100).WithMessage("Configuration type cannot exceed 100 characters.");

        RuleFor(x => x.ConfigurationJson).NotEmpty().WithMessage("Configuration JSON is required.");
    }
}