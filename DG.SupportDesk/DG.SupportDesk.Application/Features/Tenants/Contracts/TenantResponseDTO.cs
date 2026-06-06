using DG.SupportDesk.Application.Dtos.Common;
using System.ComponentModel.DataAnnotations;

namespace DG.SupportDesk.Application.Features.Tenants.Contracts;

public class TenantResponseDTO //: BaseResponseDTO<Guid>
{
    public required Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public Guid TenantTypeId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsActive { get; set; }

    public string? TenantTypeName { get; set; }
}