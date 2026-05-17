namespace DG.SupportDesk.Domain.Models.Support;

public class SupportTicketAttachment : BaseEntity
{
    public long TenantId { get; set; }
    public long SupportTicketId { get; set; }

    public string FileName { get; set; } = null!;
    public string FileUrl { get; set; } = null!;
    public string? ContentType { get; set; }
    public long? FileSizeInBytes { get; set; }

    public long? UploadedByUserId { get; set; }

    public SupportTicket SupportTicket { get; set; } = null!;
}