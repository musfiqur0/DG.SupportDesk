namespace DG.SupportDesk.Domain.Enums;

public enum TicketStatusType
{
    Submitted = 1,
    AdministrationReview = 2,
    SupportReview = 3,
    InProgress = 4,
    WaitingForClient = 5,
    Resolved = 6,
    Closed = 7,
    Reopened = 8,
    Cancelled = 9
}