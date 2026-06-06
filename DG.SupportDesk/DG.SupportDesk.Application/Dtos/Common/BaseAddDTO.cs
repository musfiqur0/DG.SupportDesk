namespace DG.SupportDesk.Application.Dtos.Common;

public class BaseAddDTO
{
    public Guid TenantId { get; set; }
    public bool IsActive { get; set; }

    public Guid? CreatedBy { get; set; }
}
