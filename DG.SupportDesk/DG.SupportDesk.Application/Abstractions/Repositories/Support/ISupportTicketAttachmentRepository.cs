using DG.SupportDesk.Domain.Models.Support;

namespace DG.SupportDesk.Application.Abstractions.Repositories.Support;

public interface ISupportTicketAttachmentRepository
{
    Task<SupportTicketAttachment> Add(SupportTicketAttachment entity, CancellationToken ct = default);
    Task<List<SupportTicketAttachment>> GetByTicketId(
        long supportTicketId,
        long tenantId,
        CancellationToken ct = default);
    Task<bool> Delete(long id, long tenantId, CancellationToken ct = default);
    Task<bool> HardDelete(long id, long tenantId, CancellationToken ct = default);
}
