using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Domain.Enums;

namespace DG.SupportDesk.Application.Dtos.Support;

public class SupportTicketResponseDTO : BaseResponseDTO<long>
{
    //public long Id { get; set; }
    //public long TenantId { get; set; }

    public long IssueNo { get; set; }
    public string? TicketCode { get; set; }

    public long ProductProjectId { get; set; }
    public string? ProductProjectName { get; set; }

    public string IssueName { get; set; } = null!;
    public string IssueTitle { get; set; } = null!;
    public string IssueDescription { get; set; } = null!;

    public DateTime IssueDate { get; set; }

    public IssueCategoryType IssueCategoryTypeId { get; set; }
    public PriorityType PriorityTypeId { get; set; }
    public SupportLevelType SupportLevelTypeId { get; set; }
    public TicketStatusType TicketStatusTypeId { get; set; }

    public long? IssuerUserId { get; set; }
    public string? IssuerPhoneNo { get; set; }
    public string? IssuerEmail { get; set; }

    public long? ResolverUserId { get; set; }
    public string? ResolverPhoneNo { get; set; }
    public string? ResolverEmail { get; set; }

    public string? Remarks { get; set; }

    public DateTime? IssueFinishingDate { get; set; }

    public int? EstimatedHours { get; set; }

    public int CommentCount { get; set; }
    public int AttachmentCount { get; set; }
}