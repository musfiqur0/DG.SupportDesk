using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.TenantConfigurations.Contracts;
using DG.SupportDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.CreateTenantConfiguration;

public class CreateTenantConfigurationHandler
{
    private readonly ISupportDeskDbContext _db;

    public CreateTenantConfigurationHandler(ISupportDeskDbContext db) => _db = db;

    public async Task<ServiceResponse<TenantConfigurationResponse>> Handle(
        CreateTenantConfigurationCommand command, CancellationToken ct)
    {
        var tenantExists = await _db.Tenants.AnyAsync(x => x.Id == command.TenantId && x.IsActive, ct);
        if (!tenantExists)
            return ServiceResponse<TenantConfigurationResponse>.ErrorResponse("Tenant not found.");

        // Ensure a tenant doesn't have duplicate configuration types
        var isExists = await _db.TenantConfigurations.AnyAsync(x =>
            x.TenantId == command.TenantId && x.ConfigurationType == command.ConfigurationType && x.IsActive, ct);

        if (isExists)
            return ServiceResponse<TenantConfigurationResponse>.ErrorResponse("Configuration type already exists for this tenant.");

        var entity = new TenantConfiguration
        {
            Id = Guid.NewGuid(),
            TenantId = command.TenantId,
            ConfigurationType = command.ConfigurationType.Trim(),
            ConfigurationJson = command.ConfigurationJson,
            IsActive = command.IsActive,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = command.CreatedBy
        };

        _db.TenantConfigurations.Add(entity);
        await _db.SaveChangesAsync(ct);

        var addedEntity = await _db.TenantConfigurations.AsNoTracking().Include(x => x.Tenant).FirstAsync(x => x.Id == entity.Id, ct);

        return ServiceResponse<TenantConfigurationResponse>.SuccessResponse(
            TenantConfigurationMapper.ToResponse(addedEntity), "Tenant configuration created successfully.");
    }
}