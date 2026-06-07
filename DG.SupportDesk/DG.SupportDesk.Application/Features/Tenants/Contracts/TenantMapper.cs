using DG.SupportDesk.Domain.Models;

namespace DG.SupportDesk.Application.Features.Tenants.Contracts;

public static class TenantMapper
{
    public static TenantResponse ToResponse(Tenant entity)
    {
        return new TenantResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Code = entity.Code,
            TenantTypeId = entity.TenantTypeId,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy,
            IsActive = entity.IsActive,
            TenantTypeName = entity.TenantType?.Name
        };
    }
}