using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.Tenants.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.Tenants.Queries.GetTenantById;

public class GetTenantByIdHandler
{
    private readonly ISupportDeskDbContext _db;

    public GetTenantByIdHandler(ISupportDeskDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResponseDTO<TenantResponseDTO>> Handle(
        GetTenantByIdQuery query,
        CancellationToken ct)
    {
        var entity = await _db.Tenants
            .AsNoTracking()
            .Where(x => x.Id == query.Id && x.IsActive)
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
            .FirstOrDefaultAsync(ct);

        if (entity is null)
        {
            return ServiceResponseDTO<TenantResponseDTO>
                .ErrorResponse("Tenant not found.");
        }

        return ServiceResponseDTO<TenantResponseDTO>
            .SuccessResponse(entity);
    }
}
