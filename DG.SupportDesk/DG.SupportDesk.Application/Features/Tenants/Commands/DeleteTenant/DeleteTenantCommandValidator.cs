using FluentValidation;

namespace DG.SupportDesk.Application.Features.Tenants.Commands.DeleteTenant;

public sealed class DeleteTenantCommandValidator : AbstractValidator<DeleteTenantCommand>
{
    public DeleteTenantCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Tenant id is required.");
    }
}