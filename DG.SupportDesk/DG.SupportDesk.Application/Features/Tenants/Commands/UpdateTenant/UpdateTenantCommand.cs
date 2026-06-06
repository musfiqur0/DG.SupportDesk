namespace DG.SupportDesk.Application.Features.Tenants.Commands.UpdateTenant;

public sealed record UpdateTenantCommand(Guid Id, string Name, string Code, Guid TenantTypeId, bool IsActive, Guid? UpdatedBy);