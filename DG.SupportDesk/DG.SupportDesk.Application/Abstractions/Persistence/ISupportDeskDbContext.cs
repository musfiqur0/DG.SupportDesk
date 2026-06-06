using DG.SupportDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Application.Abstractions.Persistence;

public interface ISupportDeskDbContext
{
    DbSet<Tenant> Tenants { get; }
    DbSet<TenantType> TenantTypes { get; }
    DbSet<IssueCategoryType> IssueCategoryTypes { get; }
    DbSet<PriorityType> PriorityTypes { get; }
    DbSet<SupportLevelType> SupportLevelTypes { get; }
    DbSet<TicketStatusType> TicketStatusTypes { get; }
    DbSet<TenantConfiguration> TenantConfigurations { get; }
    DbSet<SupportTicket> SupportTickets { get; }
    DbSet<SupportTicketComment> SupportTicketComments { get; }
    DbSet<SupportTicketAttachment> SupportTicketAttachments { get; }
    DbSet<SupportTicketStatusHistory> SupportTicketStatusHistories { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}