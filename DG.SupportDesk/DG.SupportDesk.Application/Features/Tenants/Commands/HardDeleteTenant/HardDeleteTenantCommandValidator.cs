using FluentValidation;

namespace DG.SupportDesk.Application.Features.Tenants.Commands.HardDeleteTenant;

public sealed class HardDeleteTenantCommandValidator : AbstractValidator<HardDeleteTenantCommand>
{
    public HardDeleteTenantCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Tenant id is required.");
    }
}