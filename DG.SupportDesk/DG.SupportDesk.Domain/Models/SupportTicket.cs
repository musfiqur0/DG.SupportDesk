using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DG.SupportDesk.Domain.Models;

public class SupportTicket : BaseEntity
{
    [Required]
    public Guid TenantId { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long IssueNo { get; set; }
    [MaxLength(50)]
    public string? TicketCode { get; set; }
    [Required]
    [MaxLength(300)]
    public string IssueTitle { get; set; } = null!;
    [Required]
    [MaxLength(5000)]
    public string IssueDescription { get; set; } = null!;
    [Required]
    public DateTime IssueDate { get; set; } = DateTime.UtcNow;
    [Required]
    public Guid IssueCategoryTypeId { get; set; }
    [Required]
    public Guid PriorityTypeId { get; set; }
    [Required]
    public Guid SupportLevelTypeId { get; set; }
    public Guid? ResolverUserId { get; set; }
    [Required]
    public Guid TicketStatusTypeId { get; set; }
    [MaxLength(1000)]
    public string? Remarks { get; set; }
    public DateTime? IssueClosingDate { get; set; }
    [Range(0, int.MaxValue, ErrorMessage = "Estimated hours cannot be negative.")]
    public int? EstimatedHours { get; set; }

    public Tenant Tenant { get; set; } = null!;
    public IssueCategoryType IssueCategoryType { get; set; } = null!;
    public PriorityType PriorityType { get; set; } = null!;
    public SupportLevelType SupportLevelType { get; set; } = null!;
    public TicketStatusType TicketStatusType { get; set; } = null!;
    public ICollection<SupportTicketComment> Comments { get; set; } = new List<SupportTicketComment>();
    public ICollection<SupportTicketAttachment> Attachments { get; set; } = new List<SupportTicketAttachment>();
    public ICollection<SupportTicketStatusHistory> StatusHistories { get; set; } = new List<SupportTicketStatusHistory>();
}
