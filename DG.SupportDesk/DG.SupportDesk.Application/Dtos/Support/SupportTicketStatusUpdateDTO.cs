using DG.SupportDesk.Application.Dtos.Common;
using DG.SupportDesk.Domain.Enums;

namespace DG.SupportDesk.Application.Dtos.Support;

public class SupportTicketStatusUpdateDTO : BaseUpdateDTO<long>
{
    public TicketStatusType TicketStatusTypeId { get; set; }
    public long? ChangedByUserId { get; set; }
    public string? Remarks { get; set; }
}