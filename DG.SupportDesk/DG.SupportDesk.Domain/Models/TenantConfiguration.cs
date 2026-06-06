using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DG.SupportDesk.Domain.Models;

public class TenantConfiguration : BaseEntity
{
    public Guid TenantId { get; set; }

    [Required]
    [MaxLength(100)]
    public string ConfigurationType { get; set; } = null!;

    [Required]
    [Column(TypeName = "jsonb")]
    public string ConfigurationJson { get; set; } = null!;

    public Tenant Tenant { get; set; } = null!;
}