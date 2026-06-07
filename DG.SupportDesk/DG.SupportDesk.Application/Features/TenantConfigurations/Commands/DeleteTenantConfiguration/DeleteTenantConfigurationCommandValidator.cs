using FluentValidation;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.DeleteTenantConfiguration;

public sealed class DeleteTenantConfigurationCommandValidator : AbstractValidator<DeleteTenantConfigurationCommand>
{
    public DeleteTenantConfigurationCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Tenant configuration id is required.");
    }
}