namespace DG.SupportDesk.Domain;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public long? UpdatedBy { get; set; }

    // 1 = Active, 0 = Deleted/Inactive
    public long StatusTypeId { get; set; } = 1;
}
