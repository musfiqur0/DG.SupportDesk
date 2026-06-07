using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.DeleteTenantConfiguration;

public class DeleteTenantConfigurationHandler
{
    private readonly ISupportDeskDbContext _db;

    public DeleteTenantConfigurationHandler(ISupportDeskDbContext db) => _db = db;

    public async Task<ServiceResponse<bool>> Handle(DeleteTenantConfigurationCommand command, CancellationToken ct)
    {
        var existingEntity = await _db.TenantConfigurations.FirstOrDefaultAsync(x => x.Id == command.Id && x.IsActive, ct);
        if (existingEntity is null)
            return ServiceResponse<bool>.ErrorResponse("Tenant configuration not found or already deleted.");

        existingEntity.IsActive = false;
        existingEntity.UpdatedAt = DateTime.UtcNow;
        existingEntity.UpdatedBy = command.UpdatedBy;

        await _db.SaveChangesAsync(ct);

        return ServiceResponse<bool>.SuccessResponse(true, "Tenant configuration deleted successfully.");
    }
}