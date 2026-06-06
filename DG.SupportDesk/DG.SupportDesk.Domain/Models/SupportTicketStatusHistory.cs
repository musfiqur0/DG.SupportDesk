namespace DG.SupportDesk.Domain.Models;

public class SupportTicketStatusHistory : BaseEntity
{
    public Guid TenantId { get; set; }
    public Guid SupportTicketId { get; set; }

    public Guid? FromTicketStatusTypeId { get; set; }
    public Guid ToTicketStatusTypeId { get; set; }

    public Guid? ChangedByUserId { get; set; }

    public string? Remarks { get; set; }

    public SupportTicket SupportTicket { get; set; } = null!;
}
