using FluentValidation;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.CreateTenantConfiguration;

public sealed class CreateTenantConfigurationCommandValidator : AbstractValidator<CreateTenantConfigurationCommand>
{
    public CreateTenantConfigurationCommandValidator()
    {
        RuleFor(x => x.TenantId).NotEmpty().WithMessage("Tenant id is required.");

        RuleFor(x => x.ConfigurationType)
            .Cascade(CascadeMode.Stop).NotEmpty().WithMessage("Configuration type is required.")
            .MaximumLength(100).WithMessage("Configuration type cannot exceed 100 characters.");

        RuleFor(x => x.ConfigurationJson).NotEmpty().WithMessage("Configuration JSON is required.");
    }
}