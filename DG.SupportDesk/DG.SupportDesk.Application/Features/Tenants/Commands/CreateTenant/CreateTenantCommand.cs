namespace DG.SupportDesk.Application.Features.Tenants.Commands.CreateTenant;

public sealed record CreateTenantCommand(string Name, string Code, Guid TenantTypeId, bool IsActive, Guid? CreatedBy);