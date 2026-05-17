using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Domain.Models.Support;
using DG.SupportDesk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Repositories;

public class ProductProjectRepository : IProductProjectRepository
{
    private readonly ApplicationDbContext _db;

    public ProductProjectRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<ProductProject> Add(ProductProject entity, CancellationToken ct = default)
    {
        _db.ProductProjects.Add(entity);
        await _db.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<List<ProductProject>> AddBulk(List<ProductProject> entities, CancellationToken ct = default)
    {
        await _db.ProductProjects.AddRangeAsync(entities, ct);
        await _db.SaveChangesAsync(ct);
        return entities;
    }

    public async Task<ProductProject?> GetById(long id, long tenantId, CancellationToken ct = default)
    {
        return await _db.ProductProjects
            .AsNoTracking()
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId &&
                x.StatusTypeId == 1,
                ct);
    }

    public async Task<(List<ProductProject> Items, int TotalCount)> GetAll(
        long tenantId,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
    {
        IQueryable<ProductProject> query = _db.ProductProjects
            .AsNoTracking()
            .Where(x => x.TenantId == tenantId && x.StatusTypeId == 1);

        query = query.OrderByDescending(x => x.Id);

        var total = await query.CountAsync(ct);

        if (pageNumber.HasValue && pageSize.HasValue && pageNumber > 0 && pageSize > 0)
        {
            query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
        }

        var items = await query.ToListAsync(ct);

        return (items, total);
    }

    public async Task<ProductProject?> Update(ProductProject entity, CancellationToken ct = default)
    {
        var existing = await _db.ProductProjects
            .FirstOrDefaultAsync(x =>
                x.Id == entity.Id &&
                x.TenantId == entity.TenantId &&
                x.StatusTypeId == 1,
                ct);

        if (existing is null) return null;

        existing.Name = entity.Name;
        existing.Code = entity.Code;
        existing.Description = entity.Description;
        existing.UpdatedAt = entity.UpdatedAt;
        existing.UpdatedBy = entity.UpdatedBy;
        existing.StatusTypeId = entity.StatusTypeId;

        await _db.SaveChangesAsync(ct);

        return existing;
    }

    public async Task<bool> Delete(long id, long tenantId, CancellationToken ct = default)
    {
        var existing = await _db.ProductProjects
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId &&
                x.StatusTypeId == 1,
                ct);

        if (existing is null) return false;

        existing.StatusTypeId = 0;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> HardDelete(long id, long tenantId, CancellationToken ct = default)
    {
        var existing = await _db.ProductProjects
            .FirstOrDefaultAsync(x => x.Id == id && x.TenantId == tenantId, ct);

        if (existing is null) return false;

        _db.ProductProjects.Remove(existing);
        await _db.SaveChangesAsync(ct);

        return true;
    }
}