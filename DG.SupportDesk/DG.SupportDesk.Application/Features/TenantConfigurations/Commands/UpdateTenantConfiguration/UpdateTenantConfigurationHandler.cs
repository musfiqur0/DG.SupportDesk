using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.TenantConfigurations.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.UpdateTenantConfiguration;

public class UpdateTenantConfigurationHandler
{
    private readonly ISupportDeskDbContext _db;

    public UpdateTenantConfigurationHandler(ISupportDeskDbContext db) => _db = db;

    public async Task<ServiceResponse<TenantConfigurationResponse>> Handle(
        UpdateTenantConfigurationCommand command, CancellationToken ct)
    {
        var existingEntity = await _db.TenantConfigurations.FirstOrDefaultAsync(x => x.Id == command.Id && x.IsActive, ct);
        if (existingEntity is null)
            return ServiceResponse<TenantConfigurationResponse>.ErrorResponse("Tenant configuration not found.");

        if (existingEntity.ConfigurationType != command.ConfigurationType)
        {
            var isDuplicate = await _db.TenantConfigurations.AnyAsync(x =>
                x.TenantId == existingEntity.TenantId && x.ConfigurationType == command.ConfigurationType &&
                x.IsActive && x.Id != command.Id, ct);

            if (isDuplicate)
                return ServiceResponse<TenantConfigurationResponse>.ErrorResponse("Configuration type already exists for this tenant.");
        }

        existingEntity.ConfigurationType = command.ConfigurationType.Trim();
        existingEntity.ConfigurationJson = command.ConfigurationJson;
        existingEntity.IsActive = command.IsActive;
        existingEntity.UpdatedAt = DateTime.UtcNow;
        existingEntity.UpdatedBy = command.UpdatedBy;

        await _db.SaveChangesAsync(ct);

        var updatedEntity = await _db.TenantConfigurations.Include(x => x.Tenant).AsNoTracking().FirstAsync(x => x.Id == command.Id, ct);

        return ServiceResponse<TenantConfigurationResponse>.SuccessResponse(
            TenantConfigurationMapper.ToResponse(updatedEntity), "Tenant configuration updated successfully.");
    }
}