namespace DG.SupportDesk.Domain.Models;

public class TenantType : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }

    public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
}