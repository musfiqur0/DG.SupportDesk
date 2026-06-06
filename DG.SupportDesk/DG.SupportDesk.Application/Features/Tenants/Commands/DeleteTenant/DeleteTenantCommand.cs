namespace DG.SupportDesk.Application.Features.Tenants.Commands.DeleteTenant;

public sealed record DeleteTenantCommand(Guid Id, Guid? UpdatedBy);
