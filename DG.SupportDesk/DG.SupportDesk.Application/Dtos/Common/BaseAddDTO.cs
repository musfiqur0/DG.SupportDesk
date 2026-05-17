namespace DG.SupportDesk.Application.Dtos.Common;

public class BaseAddDTO
{
    public long TenantId { get; set; }
    public long StatusTypeId { get; set; } = 1;

    public long? CreatedBy { get; set; }
}
