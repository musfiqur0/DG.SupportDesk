using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Application.Dtos.Common;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Features.Tenants.Commands.HardDeleteTenant;

public class HardDeleteTenantHandler
{
    private readonly ISupportDeskDbContext _db;

    public HardDeleteTenantHandler(ISupportDeskDbContext db)
    {
        _db = db;
    }

    public async Task<ServiceResponse<bool>> Handle(
        HardDeleteTenantCommand command,
        CancellationToken ct)
    {
        var existingEntity = await _db.Tenants
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct);

        if (existingEntity is null)
            return ServiceResponse<bool>
                .ErrorResponse("Tenant not found.");

        _db.Tenants.Remove(existingEntity);

        await _db.SaveChangesAsync(ct);

        return ServiceResponse<bool>.SuccessResponse(
            true,
            "Tenant permanently deleted successfully.");
    }
}
