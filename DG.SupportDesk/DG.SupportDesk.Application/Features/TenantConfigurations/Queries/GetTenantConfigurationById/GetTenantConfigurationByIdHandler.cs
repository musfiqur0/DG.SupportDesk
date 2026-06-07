using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.TenantConfigurations.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Queries.GetTenantConfigurationById;

public class GetTenantConfigurationByIdHandler
{
    private readonly ISupportDeskDbContext _db;

    public GetTenantConfigurationByIdHandler(ISupportDeskDbContext db) => _db = db;

    public async Task<ServiceResponse<TenantConfigurationResponse>> Handle(
        GetTenantConfigurationByIdQuery query, CancellationToken ct)
    {
        var entity = await _db.TenantConfigurations.AsNoTracking()
            .Where(x => x.Id == query.Id && x.IsActive)
            .Include(x => x.Tenant)
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
            .FirstOrDefaultAsync(ct);

        if (entity is null)
            return ServiceResponse<TenantConfigurationResponse>.ErrorResponse("Tenant configuration not found.");

        return ServiceResponse<TenantConfigurationResponse>.SuccessResponse(entity);
    }
}