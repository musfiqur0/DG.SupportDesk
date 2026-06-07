using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.Tenants.Commands.DeleteTenant;

public class DeleteTenantHandler
{
    private readonly ISupportDeskDbContext _db;

    public DeleteTenantHandler(ISupportDeskDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResponse<bool>> Handle(
        DeleteTenantCommand command,
        CancellationToken ct)
    {
        var existingEntity = await _db.Tenants
            .FirstOrDefaultAsync(x => x.Id == command.Id && x.IsActive, ct);

        if (existingEntity is null)
        {
            return ServiceResponse<bool>
                .ErrorResponse("Tenant not found or already deleted.");
        }

        // 2. Perform Soft Delete
        existingEntity.IsActive = false;
        existingEntity.UpdatedAt = DateTime.UtcNow;
        existingEntity.UpdatedBy = command.UpdatedBy;

        // 3. Save changes
        await _db.SaveChangesAsync(ct);

        return ServiceResponse<bool>.SuccessResponse(
            true,
            "Tenant deleted successfully.");
    }
}
