using System.ComponentModel.DataAnnotations;

namespace DG.SupportDesk.Application.Dtos.Common;

public class BaseResponseDTO<PKDataType>
{
    public required PKDataType Id { get; set; }
    public long TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public long? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }
    public long StatusTypeId { get; set; } = 1;
}
