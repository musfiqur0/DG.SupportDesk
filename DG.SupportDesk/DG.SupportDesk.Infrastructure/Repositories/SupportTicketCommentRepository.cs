using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Domain.Models.Support;
using DG.SupportDesk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Repositories;

public class SupportTicketCommentRepository : ISupportTicketCommentRepository
{
    private readonly ApplicationDbContext _db;

    public SupportTicketCommentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SupportTicketComment> Add(
        SupportTicketComment entity,
        CancellationToken ct = default)
    {
        _db.SupportTicketComments.Add(entity);
        await _db.SaveChangesAsync(ct);

        return entity;
    }

    public async Task<List<SupportTicketComment>> GetByTicketId(
        long supportTicketId,
        long tenantId,
        CancellationToken ct = default)
    {
        return await _db.SupportTicketComments
            .AsNoTracking()
            .Where(x =>
                x.SupportTicketId == supportTicketId &&
                x.TenantId == tenantId &&
                x.StatusTypeId == 1)
            .OrderByDescending(x => x.Id)
            .ToListAsync(ct);
    }

    public async Task<bool> Delete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var existing = await _db.SupportTicketComments
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId &&
                x.StatusTypeId == 1,
                ct);

        if (existing is null)
        {
            return false;
        }

        existing.StatusTypeId = 0;
        existing.UpdatedAt = DateTime.UtcNow;

        await _db.SaveChangesAsync(ct);

        return true;
    }

    public async Task<bool> HardDelete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var existing = await _db.SupportTicketComments
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId,
                ct);

        if (existing is null)
        {
            return false;
        }

        _db.SupportTicketComments.Remove(existing);
        await _db.SaveChangesAsync(ct);

        return true;
    }
}