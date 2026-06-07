using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Application.Features.Tenants.Contracts;
using DG.SupportDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.Tenants.Commands.CreateTenant;

public class CreateTenantHandler
{
    private readonly ISupportDeskDbContext _db;

    public CreateTenantHandler(ISupportDeskDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResponse<TenantResponse>> Handle(
        CreateTenantCommand command,
        CancellationToken ct)
    {
        var exists = await _db.Tenants.AnyAsync(x =>
            x.Code == command.Code && x.IsActive,
            ct);

        if (exists)
            return ServiceResponse<TenantResponse>
                .ErrorResponse("Tenant code already exists.");

        var tenantType = await _db.TenantTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == command.TenantTypeId && x.IsActive, ct);

        if (tenantType is null)
            return ServiceResponse<TenantResponse>
                .ErrorResponse("Tenant type not found.");

        var entity = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = command.Name.Trim(),
            Code = command.Code,
            TenantTypeId = command.TenantTypeId,
            IsActive = command.IsActive,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = command.CreatedBy
        };

        _db.Tenants.Add(entity);
        await _db.SaveChangesAsync(ct);

        var addedEntity = await _db.Tenants
            .AsNoTracking()
            .Include(x => x.TenantType)
            .FirstAsync(x => x.Id == entity.Id, ct);

        return ServiceResponse<TenantResponse>.SuccessResponse(
            TenantMapper.ToResponse(addedEntity),
            "Tenant created successfully.");
    }
}