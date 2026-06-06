using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.Tenants.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.Tenants.Queries.GetTenants;

public class GetTenantsHandler
{
    private readonly ISupportDeskDbContext _db;

    public GetTenantsHandler(ISupportDeskDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResponseDTO<PagedResponseDTO<TenantResponseDTO>>> Handle(
        GetTenantsQuery query,
        CancellationToken ct)
    {
        var pageNumber = query.PageNumber ?? 1;
        var pageSize = query.PageSize ?? 10;

        var entityQuery = _db.Tenants
            .AsNoTracking()
            .Where(x => x.IsActive);

        var totalCount = await entityQuery.CountAsync(ct);

        var items = await entityQuery
            .OrderByDescending(x => x.CreatedAt)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(x => new TenantResponseDTO
            {
                Id = x.Id,
                Name = x.Name,
                Code = x.Code,
                TenantTypeId = x.TenantTypeId,
                TenantTypeName = x.TenantType.Name,
                IsActive = x.IsActive,
                CreatedAt = x.CreatedAt,
                CreatedBy = x.CreatedBy,
                UpdatedAt = x.UpdatedAt,
                UpdatedBy = x.UpdatedBy
            })
            .ToListAsync(ct);

        var response = new PagedResponseDTO<TenantResponseDTO>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            Items = items
        };

        return ServiceResponseDTO<PagedResponseDTO<TenantResponseDTO>>
            .SuccessResponse(response);
    }
}