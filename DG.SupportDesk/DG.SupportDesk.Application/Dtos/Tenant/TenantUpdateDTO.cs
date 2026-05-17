using DG.SupportDesk.Application.Dtos.Common;

namespace DG.SupportDesk.Application.Dtos.Tenant;

public class TenantUpdateDTO : BaseUpdateDTO<long>
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
}