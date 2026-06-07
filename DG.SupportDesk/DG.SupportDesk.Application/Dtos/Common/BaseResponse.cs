namespace DG.SupportDesk.Application.Dtos.Common;

public class BaseResponse<PKDataType>
{
    public required PKDataType Id { get; set; }
    public Guid TenantId { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Guid? UpdatedBy { get; set; }
    public bool IsActive { get; set; }
}
