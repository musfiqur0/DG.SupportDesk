using DG.SupportDesk.Application.Dtos.Common;

namespace DG.SupportDesk.Application.Dtos.Support;

public class ProductProjectUpdateDTO : BaseUpdateDTO<long>
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public string? Description { get; set; }
}
