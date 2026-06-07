namespace DG.SupportDesk.Application.Dtos.Common;

public class BaseUpdate<PKDataType>
{
    public required PKDataType Id { get; set; }
    public Guid TenantId { get; set; }
    public bool IsActive { get; set; }
    public Guid? UpdatedBy { get; set; }
}
