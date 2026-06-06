using DG.SupportDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Persistence.Seeds;

public class SeedData
{
    public void Seed(ApplicationDbContext context)
    {
        if (!context.Database.CanConnect())
            return;

        if (context.Database.GetPendingMigrations().Any())
            return;

        SeedTenantTypes(context);
    }

    private void SeedTenantTypes(ApplicationDbContext context)
    {
        if (context.TenantTypes.Any())
        {
            return;
        }

        var tenantTypes = new List<TenantType>
        {
            new TenantType
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Internal",
                Code = "INTERNAL",
                Description = "Internal tenant type",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TenantType
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "External",
                Code = "EXTERNAL",
                Description = "External tenant type",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            }
        };

        context.TenantTypes.AddRange(tenantTypes);
        context.SaveChanges();
    }
}