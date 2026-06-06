namespace DG.SupportDesk.Domain.Models;

public class SupportTicketComment : BaseEntity
{
    public Guid TenantId { get; set; }
    public Guid SupportTicketId { get; set; }
    public Guid? ParentCommentId { get; set; }

    //public Guid? CommentedByUserId { get; set; }

    public string Comment { get; set; } = null!;

    public bool IsPublic { get; set; } = true;

    public SupportTicket SupportTicket { get; set; } = null!;
}