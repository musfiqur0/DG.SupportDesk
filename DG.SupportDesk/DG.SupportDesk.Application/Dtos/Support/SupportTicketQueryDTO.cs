using DG.SupportDesk.Domain.Enums;

namespace DG.SupportDesk.Application.Dtos.Support;

public class SupportTicketQueryDTO
{
    public long TenantId { get; set; }

    public long? ProductProjectId { get; set; }
    public TicketStatusType? TicketStatusTypeId { get; set; }
    public PriorityType? PriorityTypeId { get; set; }
    public SupportLevelType? SupportLevelTypeId { get; set; }

    public string? SearchText { get; set; }

    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }
}