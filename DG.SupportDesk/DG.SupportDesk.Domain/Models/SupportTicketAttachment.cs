namespace DG.SupportDesk.Domain.Models;

public class SupportTicketAttachment : BaseEntity
{
    public Guid TenantId { get; set; }
    public Guid SupportTicketId { get; set; }

    public string FileName { get; set; } = null!;
    public string FileUrl { get; set; } = null!;
    //public string? ContentType { get; set; }
    //public long? FileSizeInBytes { get; set; }

    public Guid? UploadedByUserId { get; set; }

    public SupportTicket SupportTicket { get; set; } = null!;
}