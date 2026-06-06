using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DG.SupportDesk.Domain.Models;

public class Tenant : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public Guid TenantTypeId { get; set; }

    public TenantType TenantType { get; set; } = null!;
}