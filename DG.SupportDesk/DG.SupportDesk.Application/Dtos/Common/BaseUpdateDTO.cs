namespace DG.SupportDesk.Application.Dtos.Common;

public class BaseUpdateDTO<PKDataType>
{
    public required PKDataType Id { get; set; }
    public long TenantId { get; set; }
    public long StatusTypeId { get; set; } = 1;
    public long? UpdatedBy { get; set; }
}
