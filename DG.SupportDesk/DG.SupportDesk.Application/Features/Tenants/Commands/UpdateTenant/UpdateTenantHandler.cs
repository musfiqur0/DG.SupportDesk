using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.Tenants.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.Tenants.Commands.UpdateTenant;

public class UpdateTenantHandler
{
    private readonly ISupportDeskDbContext _db;

    public UpdateTenantHandler(ISupportDeskDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResponseDTO<TenantResponseDTO>> Handle(
        UpdateTenantCommand command,
        CancellationToken ct)
    {
        var existingEntity = await _db.Tenants
            .Include(x => x.TenantType)
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.IsActive, ct);

        if (existingEntity is null)
            return ServiceResponseDTO<TenantResponseDTO>
                .ErrorResponse("Tenant not found.");

        var IsDuplicate = await _db.Tenants
            .AnyAsync(x => x.Code == command.Code && x.IsActive && x.Id != command.Id, ct);

        if (IsDuplicate)
            return ServiceResponseDTO<TenantResponseDTO>
                .ErrorResponse("Tenant code already exists.");

        var IsTenantTypeExists = await _db.TenantTypes
           .AnyAsync(x => x.Id == command.TenantTypeId && x.IsActive, ct);

        if (!IsTenantTypeExists)
            return ServiceResponseDTO<TenantResponseDTO>
                .ErrorResponse("Tenant type not found.");

        existingEntity.Name = command.Name.Trim();
        existingEntity.Code = command.Code;
        existingEntity.TenantTypeId = command.TenantTypeId;
        existingEntity.IsActive = command.IsActive;
        existingEntity.UpdatedAt = DateTime.UtcNow;
        existingEntity.UpdatedBy = command.UpdatedBy;

        await _db.SaveChangesAsync(ct);

        var updatedEntity = await _db.Tenants
            .AsNoTracking()
            .Include(x => x.TenantType)
            .FirstAsync(x => x.Id == command.Id, ct);

        return ServiceResponseDTO<TenantResponseDTO>.SuccessResponse(
            TenantMapper.ToResponse(updatedEntity),
            "Tenant updated successfully.");
    }
}