namespace DG.SupportDesk.Application.Features.Tenants.Queries.GetTenants;

public sealed record GetTenantsQuery(int? PageNumber = null, int? PageSize = null);