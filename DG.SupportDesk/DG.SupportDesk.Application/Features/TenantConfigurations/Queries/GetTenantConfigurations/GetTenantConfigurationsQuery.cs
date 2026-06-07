namespace DG.SupportDesk.Application.Features.TenantConfigurations.Queries.GetTenantConfigurations;

public sealed record GetTenantConfigurationsQuery(Guid? TenantId = null, int? PageNumber = null, int? PageSize = null);