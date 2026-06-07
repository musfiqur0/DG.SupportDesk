namespace DG.SupportDesk.Domain.Models;

public class TenantConfiguration : BaseEntity
{
    public Guid TenantId { get; set; }
    public string ConfigurationType { get; set; } = null!;
    public string ConfigurationJson { get; set; } = null!;

    public Tenant Tenant { get; set; } = null!;
}