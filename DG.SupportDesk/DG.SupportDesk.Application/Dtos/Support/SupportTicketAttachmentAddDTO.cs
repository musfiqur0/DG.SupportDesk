using DG.SupportDesk.Application.Dtos.Common;

namespace DG.SupportDesk.Application.Dtos.Support;

public class SupportTicketAttachmentAddDTO : BaseAddDTO
{
    //public long TenantId { get; set; }
    public long SupportTicketId { get; set; }

    public string FileName { get; set; } = null!;
    public string FileUrl { get; set; } = null!;
    public string? ContentType { get; set; }
    public long? FileSizeInBytes { get; set; }

    public long? UploadedByUserId { get; set; }
}