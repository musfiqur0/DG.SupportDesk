using DG.SupportDesk.Domain.Models;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Contracts;

public static class TenantConfigurationMapper
{
    public static TenantConfigurationResponse ToResponse(TenantConfiguration entity)
    {
        return new TenantConfigurationResponse
        {
            Id = entity.Id,
            TenantId = entity.TenantId,
            ConfigurationType = entity.ConfigurationType,
            ConfigurationJson = entity.ConfigurationJson,
            CreatedAt = entity.CreatedAt,
            CreatedBy = entity.CreatedBy,
            UpdatedAt = entity.UpdatedAt,
            UpdatedBy = entity.UpdatedBy,
            TenantName = entity.Tenant.Name
        };
    }
}