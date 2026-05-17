using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Domain.Models.Support;
using DG.SupportDesk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Repositories;

public class SupportTicketStatusHistoryRepository : ISupportTicketStatusHistoryRepository
{
    private readonly ApplicationDbContext _db;

    public SupportTicketStatusHistoryRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SupportTicketStatusHistory> Add(
        SupportTicketStatusHistory entity,
        CancellationToken ct = default)
    {
        _db.SupportTicketStatusHistories.Add(entity);
        await _db.SaveChangesAsync(ct);

        return entity;
    }

    public async Task<List<SupportTicketStatusHistory>> GetByTicketId(
        long supportTicketId,
        long tenantId,
        CancellationToken ct = default)
    {
        return await _db.SupportTicketStatusHistories
            .AsNoTracking()
            .Where(x =>
                x.SupportTicketId == supportTicketId &&
                x.TenantId == tenantId &&
                x.StatusTypeId == 1)
            .OrderByDescending(x => x.Id)
            .ToListAsync(ct);
    }
}