using DG.SupportDesk.Domain.Enums;
using DG.SupportDesk.Domain.Models.Support;

namespace DG.SupportDesk.Application.Abstractions.Repositories.Support;

public interface ISupportTicketRepository
{
    Task<SupportTicket> Add(SupportTicket entity, CancellationToken ct = default);
    Task<SupportTicket?> GetById(long id, long tenantId, CancellationToken ct = default);
    Task<SupportTicket?> GetDetailsById(long id, long tenantId, CancellationToken ct = default);
    Task<(List<SupportTicket> Items, int TotalCount)> GetAll(
        long tenantId,
        long? productProjectId = null,
        TicketStatusType? ticketStatusTypeId = null,
        PriorityType? priorityTypeId = null,
        SupportLevelType? supportLevelTypeId = null,
        string? searchText = null,
        int? pageNumber = null,
        int? pageSize = null,
        CancellationToken ct = default);
    Task<SupportTicket?> Update(SupportTicket entity, CancellationToken ct = default);
    Task<SupportTicket?> UpdateStatus(
        long id,
        long tenantId,
        TicketStatusType ticketStatusTypeId,
        long? changedByUserId,
        string? remarks,
        CancellationToken ct = default);
    Task<bool> Delete(long id, long tenantId, CancellationToken ct = default);
    Task<bool> HardDelete(long id, long tenantId, CancellationToken ct = default);
}
