using DG.SupportDesk.Application.Dtos.Common;

namespace DG.SupportDesk.Application.Dtos.Support;

public class SupportTicketCommentAddDTO : BaseAddDTO
{
    //public long TenantId { get; set; }
    public long SupportTicketId { get; set; }
    public long? CommentedByUserId { get; set; }
    public string Comment { get; set; } = null!;
    public bool IsPublic { get; set; } = true;
}