namespace DG.SupportDesk.Domain.Models.Support;

public class SupportTicketComment : BaseEntity
{
    public long TenantId { get; set; }
    public long SupportTicketId { get; set; }

    public long? CommentedByUserId { get; set; }

    public string Comment { get; set; } = null!;

    public bool IsPublic { get; set; } = true;

    public SupportTicket SupportTicket { get; set; } = null!;
}