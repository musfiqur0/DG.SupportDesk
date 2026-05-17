using DG.SupportDesk.Domain.Enums;

namespace DG.SupportDesk.Domain.Models.Support;

public class SupportTicket : BaseEntity
{
    public long TenantId { get; set; }
    public long IssueNo { get; set; }
    public string? TicketCode { get; set; }
    public long ProductProjectId { get; set; }
    public string IssueName { get; set; } = null!;
    public string IssueTitle { get; set; } = null!;
    public string IssueDescription { get; set; } = null!;
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    public IssueCategoryType IssueCategoryTypeId { get; set; } = IssueCategoryType.General;
    public PriorityType PriorityTypeId { get; set; } = PriorityType.Medium;
    public SupportLevelType SupportLevelTypeId { get; set; } = SupportLevelType.FirstLevelSupport;
    public long? IssuerUserId { get; set; }
    public string? IssuerPhoneNo { get; set; }
    public string? IssuerEmail { get; set; }
    public long? ResolverUserId { get; set; }
    public string? ResolverPhoneNo { get; set; }
    public string? ResolverEmail { get; set; }
    public TicketStatusType TicketStatusTypeId { get; set; } = TicketStatusType.Submitted;
    public string? Remarks { get; set; }
    public DateTime? IssueFinishingDate { get; set; }
    public int? EstimatedHours { get; set; }

    public Tenant Tenant { get; set; } = null!;
    public ProductProject ProductProject { get; set; } = null!;

    public ICollection<SupportTicketComment> Comments { get; set; } = new List<SupportTicketComment>();
    public ICollection<SupportTicketAttachment> Attachments { get; set; } = new List<SupportTicketAttachment>();
    public ICollection<SupportTicketStatusHistory> StatusHistories { get; set; } = new List<SupportTicketStatusHistory>();
}
