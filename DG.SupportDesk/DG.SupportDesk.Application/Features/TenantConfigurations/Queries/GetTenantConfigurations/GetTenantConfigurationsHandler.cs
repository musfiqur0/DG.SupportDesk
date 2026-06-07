using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.TenantConfigurations.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Queries.GetTenantConfigurations;

public class GetTenantConfigurationsHandler
{
    private readonly ISupportDeskDbContext _db;

    public GetTenantConfigurationsHandler(ISupportDeskDbContext db) => _db = db;

    public async Task<ServiceResponse<PagedResponse<TenantConfigurationResponse>>> Handle(
        GetTenantConfigurationsQuery query, CancellationToken ct)
    {
        var pageNumber = query.PageNumber ?? 1;
        var pageSize = query.PageSize ?? 10;

        var entityQuery = _db.TenantConfigurations.AsNoTracking().Where(x => x.IsActive);

        if (query.TenantId.HasValue)
            entityQuery = entityQuery.Where(x => x.TenantId == query.TenantId.Value);

        var totalCount = await entityQuery.CountAsync(ct);

        var items = await entityQuery
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TenantConfigurationResponse
            {
                Id = x.Id,
                TenantId = x.TenantId,
                ConfigurationType = x.ConfigurationType,
                ConfigurationJson = x.ConfigurationJson,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy,
                TenantName = x.Tenant.Name
            })
            .ToListAsync(ct);

        return ServiceResponse<PagedResponse<TenantConfigurationResponse>>.SuccessResponse(new()
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = items
        });
    }
}