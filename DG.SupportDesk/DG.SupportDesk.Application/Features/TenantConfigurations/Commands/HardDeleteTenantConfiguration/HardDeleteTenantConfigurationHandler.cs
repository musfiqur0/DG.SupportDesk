using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.TenantConfigurations.Commands.HardDeleteTenantConfiguration;

public class HardDeleteTenantConfigurationHandler
{
    private readonly ISupportDeskDbContext _db;

    public HardDeleteTenantConfigurationHandler(ISupportDeskDbContext db) => _db = db;

    public async Task<ServiceResponse<bool>> Handle(HardDeleteTenantConfigurationCommand command, CancellationToken ct)
    {
        var existingEntity = await _db.TenantConfigurations.FirstOrDefaultAsync(x => x.Id == command.Id && x.TenantId == command.TenantId, ct);
        if (existingEntity is null)
            return ServiceResponse<bool>.ErrorResponse("Tenant configuration not found.");

        _db.TenantConfigurations.Remove(existingEntity);
        await _db.SaveChangesAsync(ct);

        return ServiceResponse<bool>.SuccessResponse(true, "Tenant configuration permanently deleted successfully.");
    }
}