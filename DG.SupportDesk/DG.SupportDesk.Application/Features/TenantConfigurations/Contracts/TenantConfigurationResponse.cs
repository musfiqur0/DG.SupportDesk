namespace DG.SupportDesk.Application.Features.TenantConfigurations.Contracts;

public class TenantConfigurationResponse
{
    public required Guid Id { get; set; }
    public Guid TenantId { get; set; }
    public string ConfigurationType { get; set; } = null!;
    public string ConfigurationJson { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsActive { get; set; }

    public string? TenantName { get; set; }
}
