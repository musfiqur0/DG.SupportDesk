using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Domain.Enums;

namespace DG.SupportDesk.Application.Dtos.Support;

public class SupportTicketUpdateDTO : BaseUpdateDTO<long>
{
    //public long Id { get; set; }
    //public long TenantId { get; set; }
    public long ProductProjectId { get; set; }

    public string IssueName { get; set; } = null!;
    public string IssueTitle { get; set; } = null!;
    public string IssueDescription { get; set; } = null!;

    public IssueCategoryType IssueCategoryTypeId { get; set; }
    public PriorityType PriorityTypeId { get; set; }
    public SupportLevelType SupportLevelTypeId { get; set; }

    public long? IssuerUserId { get; set; }
    public string? IssuerPhoneNo { get; set; }
    public string? IssuerEmail { get; set; }

    public long? ResolverUserId { get; set; }
    public string? ResolverPhoneNo { get; set; }
    public string? ResolverEmail { get; set; }

    public string? Remarks { get; set; }
    public int? EstimatedHours { get; set; }

    //public long? UpdatedBy { get; set; }
}