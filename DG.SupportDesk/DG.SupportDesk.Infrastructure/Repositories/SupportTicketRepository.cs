using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Domain.Enums;
using DG.SupportDesk.Domain.Models.Support;
using DG.SupportDesk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Repositories;

public class SupportTicketRepository : ISupportTicketRepository
{
    private readonly ApplicationDbContext _db;

    public SupportTicketRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<SupportTicket> Add(
        SupportTicket entity,
        CancellationToken ct = default)
    {
        _db.SupportTickets.Add(entity);
        await _db.SaveChangesAsync(ct);

        entity.TicketCode = $"DG-TCK-{entity.IssueNo:000000}";

        await _db.SaveChangesAsync(ct);

        return entity;
    }

    public async Task<SupportTicket?> GetById(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        return await _db.SupportTickets
            .AsNoTracking()
            .Include(x => x.ProductProject)
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId &&
                x.StatusTypeId == 1,
                ct);
    }

    public async Task<SupportTicket?> GetDetailsById(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        return await _db.SupportTickets
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.ProductProject)
            .Include(x => x.Comments.Where(c => c.StatusTypeId == 1))
            .Include(x => x.Attachments.Where(a => a.StatusTypeId == 1))
            .Include(x => x.StatusHistories.Where(h => h.StatusTypeId == 1))
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId &&
                x.StatusTypeId == 1,
                ct);
    }

    public async Task<(List<SupportTicket> Items, int TotalCount)> GetAll(
        long tenantId,
        long? productProjectId = null,
        TicketStatusType? ticketStatusTypeId = null,
        PriorityType? priorityTypeId = null,
        SupportLevelType? supportLevelTypeId = null,
        string? searchText = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default)
    {
        IQueryable<SupportTicket> query = _db.SupportTickets
            .AsNoTracking()
            .AsSplitQuery()
            .Include(x => x.ProductProject)
            .Include(x => x.Comments.Where(c => c.StatusTypeId == 1))
            .Include(x => x.Attachments.Where(a => a.StatusTypeId == 1))
            .Where(x => x.TenantId == tenantId && x.StatusTypeId == 1);

        if (productProjectId.HasValue)
        {
            query = query.Where(x => x.ProductProjectId == productProjectId.Value);
        }

        if (ticketStatusTypeId.HasValue)
        {
            query = query.Where(x => x.TicketStatusTypeId == ticketStatusTypeId.Value);
        }

        if (priorityTypeId.HasValue)
        {
            query = query.Where(x => x.PriorityTypeId == priorityTypeId.Value);
        }

        if (supportLevelTypeId.HasValue)
        {
            query = query.Where(x => x.SupportLevelTypeId == supportLevelTypeId.Value);
        }

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            var keyword = searchText.Trim().ToLower();

            query = query.Where(x =>
                x.IssueName.ToLower().Contains(keyword) ||
                x.IssueTitle.ToLower().Contains(keyword) ||
                x.IssueDescription.ToLower().Contains(keyword) ||
                (x.TicketCode != null && x.TicketCode.ToLower().Contains(keyword)));
        }

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

    public async Task<SupportTicket?> Update(
        SupportTicket entity,
        CancellationToken ct = default)
    {
        var existing = await _db.SupportTickets
            .FirstOrDefaultAsync(x =>
                x.Id == entity.Id &&
                x.TenantId == entity.TenantId &&
                x.StatusTypeId == 1,
                ct);

        if (existing is null)
        {
            return null;
        }

        existing.ProductProjectId = entity.ProductProjectId;

        existing.IssueName = entity.IssueName;
        existing.IssueTitle = entity.IssueTitle;
        existing.IssueDescription = entity.IssueDescription;

        existing.IssueCategoryTypeId = entity.IssueCategoryTypeId;
        existing.PriorityTypeId = entity.PriorityTypeId;
        existing.SupportLevelTypeId = entity.SupportLevelTypeId;

        existing.IssuerUserId = entity.IssuerUserId;
        existing.IssuerPhoneNo = entity.IssuerPhoneNo;
        existing.IssuerEmail = entity.IssuerEmail;

        existing.ResolverUserId = entity.ResolverUserId;
        existing.ResolverPhoneNo = entity.ResolverPhoneNo;
        existing.ResolverEmail = entity.ResolverEmail;

        existing.Remarks = entity.Remarks;
        existing.EstimatedHours = entity.EstimatedHours;

        existing.UpdatedAt = entity.UpdatedAt ?? DateTime.UtcNow;
        existing.UpdatedBy = entity.UpdatedBy;
        existing.StatusTypeId = entity.StatusTypeId;

        await _db.SaveChangesAsync(ct);

        return existing;
    }

    public async Task<SupportTicket?> UpdateStatus(
        long id,
        long tenantId,
        TicketStatusType ticketStatusTypeId,
        long? changedByUserId,
        string? remarks,
        CancellationToken ct = default)
    {
        var existing = await _db.SupportTickets
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId &&
                x.StatusTypeId == 1,
                ct);

        if (existing is null)
        {
            return null;
        }

        existing.TicketStatusTypeId = ticketStatusTypeId;
        existing.UpdatedAt = DateTime.UtcNow;
        existing.UpdatedBy = changedByUserId;

        if (ticketStatusTypeId == TicketStatusType.Resolved)
        {
            existing.IssueFinishingDate = DateTime.UtcNow;
            existing.ResolverUserId = changedByUserId;
        }

        if (!string.IsNullOrWhiteSpace(remarks))
        {
            existing.Remarks = remarks;
        }

        await _db.SaveChangesAsync(ct);

        return existing;
    }

    public async Task<bool> Delete(
        long id,
        long tenantId,
        CancellationToken ct = default)
    {
        var existing = await _db.SupportTickets
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
        var existing = await _db.SupportTickets
            .FirstOrDefaultAsync(x =>
                x.Id == id &&
                x.TenantId == tenantId,
                ct);

        if (existing is null)
        {
            return false;
        }

        _db.SupportTickets.Remove(existing);
        await _db.SaveChangesAsync(ct);

        return true;
    }
}