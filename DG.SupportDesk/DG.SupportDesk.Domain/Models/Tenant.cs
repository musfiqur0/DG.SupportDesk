using DG.SupportDesk.Domain.Models.Support;

namespace DG.SupportDesk.Domain.Models;

public class Tenant : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;

    public ICollection<ProductProject> ProductProjects { get; set; } = new List<ProductProject>();
}