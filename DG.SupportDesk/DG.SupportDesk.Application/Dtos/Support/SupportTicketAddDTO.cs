using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Domain.Enums;

namespace DG.SupportDesk.Application.Dtos.Support;

public class SupportTicketAddDTO : BaseAddDTO
{
    public long ProductProjectId { get; set; }

    public string IssueName { get; set; } = null!;
    public string IssueTitle { get; set; } = null!;
    public string IssueDescription { get; set; } = null!;

    public IssueCategoryType IssueCategoryTypeId { get; set; } = IssueCategoryType.General;
    public PriorityType PriorityTypeId { get; set; } = PriorityType.Medium;
    public SupportLevelType SupportLevelTypeId { get; set; } = SupportLevelType.FirstLevelSupport;

    public long? IssuerUserId { get; set; }
    public string? IssuerPhoneNo { get; set; }
    public string? IssuerEmail { get; set; }

    public string? Remarks { get; set; }
    public int? EstimatedHours { get; set; }
}