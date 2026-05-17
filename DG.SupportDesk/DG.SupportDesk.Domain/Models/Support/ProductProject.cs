namespace DG.SupportDesk.Domain.Models.Support;

public class ProductProject : BaseEntity
{
    public long TenantId { get; set; }

    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }

    public Tenant Tenant { get; set; } = null!;
    public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
}