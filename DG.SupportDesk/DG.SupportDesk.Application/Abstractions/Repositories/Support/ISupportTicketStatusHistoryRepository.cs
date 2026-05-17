using DG.SupportDesk.Domain.Models.Support;

namespace DG.SupportDesk.Application.Abstractions.Repositories.Support;

public interface ISupportTicketStatusHistoryRepository
{
    Task<SupportTicketStatusHistory> Add(
        SupportTicketStatusHistory entity,
        CancellationToken ct = default);
    Task<List<SupportTicketStatusHistory>> GetByTicketId(
        long supportTicketId,
        long tenantId,
        CancellationToken ct = default);
}
