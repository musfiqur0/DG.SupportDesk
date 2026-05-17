using DG.SupportDesk.Domain.Enums;

namespace DG.SupportDesk.Domain.Models.Support;

public class SupportTicketStatusHistory : BaseEntity
{
    public long TenantId { get; set; }
    public long SupportTicketId { get; set; }

    public TicketStatusType? FromTicketStatusTypeId { get; set; }
    public TicketStatusType ToTicketStatusTypeId { get; set; }

    public long? ChangedByUserId { get; set; }

    public string? Remarks { get; set; }

    public SupportTicket SupportTicket { get; set; } = null!;
}
