using DG.SupportDesk.Domain.Models.Support;

namespace DG.SupportDesk.Application.Abstractions.Repositories.Support;

public interface ISupportTicketCommentRepository
{
    Task<SupportTicketComment> Add(SupportTicketComment entity, CancellationToken ct = default);
    Task<List<SupportTicketComment>> GetByTicketId(
        long supportTicketId,
        long tenantId,
        CancellationToken ct = default);
    Task<bool> Delete(long id, long tenantId, CancellationToken ct = default);
    Task<bool> HardDelete(long id, long tenantId, CancellationToken ct = default);
}
