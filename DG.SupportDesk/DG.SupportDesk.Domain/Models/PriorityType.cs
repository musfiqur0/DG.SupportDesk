using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DG.SupportDesk.Domain.Models;

[Index(nameof(Code), IsUnique = true)]
public class PriorityType : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string Code { get; set; } = null!;

    [MaxLength(1000)]
    public string? Description { get; set; }

    public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
}