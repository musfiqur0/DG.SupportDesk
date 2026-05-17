using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Infrastructure.Persistence;
using DG.SupportDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Repositories;

public class TenantRepository : ITenantRepository
{
    private readonly ApplicationDbContext _db;

    public TenantRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<Tenant> Add(Tenant entity, CancellationToken ct = default)
    {
        _db.Tenants.Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<Tenant?> GetById(long id, CancellationToken ct = default)
    {
        return await _db.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.StatusTypeId == 1, ct);
    }

    public async Task<Tenant?> GetByCode(string code, CancellationToken ct = default)
    {
        return await _db.Tenants
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Code.ToLower() == code.ToLower() &&
                x.StatusTypeId == 1,
                ct);
    }

    public async Task<(List<Tenant> Items, int TotalCount)> GetAll(
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
    {
        IQueryable<Tenant> query = _db.Tenants
            .AsNoTracking()
            .Where(x => x.StatusTypeId == 1);

        query = query.OrderByDescending(x => x.Id);

        var total = await query.CountAsync(ct);

        if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
        {
            query = query
                .Skip((pageNumber.Value - 1) * pageSize.Value)
                .Take(pageSize.Value);
        }

        var items = await query.ToListAsync(ct);

        return (items, total);
    }

    public async Task<Tenant?> Update(Tenant entity, CancellationToken ct = default)
    {
        var existing = await _db.Tenants
            .FirstOrDefaultAsync(x => x.Id == entity.Id && x.StatusTypeId == 1, ct);

        if (existing is null) return null;

        existing.Name = entity.Name;
        existing.Code = entity.Code;
        existing.UpdatedAt = entity.UpdatedAt;
        existing.UpdatedBy = entity.UpdatedBy;
        existing.StatusTypeId = entity.StatusTypeId;

        await _db.SaveChangesAsync(ct);

        return existing;
    }

    public async Task<bool> Delete(long id, CancellationToken ct = default)
    {
        var existing = await _db.Tenants
            .FirstOrDefaultAsync(x => x.Id == id && x.StatusTypeId == 1, ct);

        if (existing is null) return false;

        existing.StatusTypeId = 0;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> HardDelete(long id, CancellationToken ct = default)
    {
        var existing = await _db.Tenants
            .FirstOrDefaultAsync(x => x.Id == id, ct);

        if (existing is null) return false;

        _db.Tenants.Remove(existing);
        await _db.SaveChangesAsync(ct);

        return true;
    }
}