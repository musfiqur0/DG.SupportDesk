using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Domain.Models.Support;
using DG.SupportDesk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Repositories;

public class SupportTicketAttachmentRepository : ISupportTicketAttachmentRepository
{
    private readonly ApplicationDbContext _db;

    public SupportTicketAttachmentRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SupportTicketAttachment> Add(
        SupportTicketAttachment entity,
        CancellationToken ct = default)
    {
        _db.SupportTicketAttachments.Add(entity);
        await _db.SaveChangesAsync(ct);

        return entity;
    }

    public async Task<List<SupportTicketAttachment>> GetByTicketId(
        long supportTicketId,
        long tenantId,
        CancellationToken ct = default)
    {
        return await _db.SupportTicketAttachments
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
        var existing = await _db.SupportTicketAttachments
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
        var existing = await _db.SupportTicketAttachments
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId,
                ct);

        if (existing is null)
        {
            return false;
        }

        _db.SupportTicketAttachments.Remove(existing);
        await _db.SaveChangesAsync(ct);

        return true;
    }
}