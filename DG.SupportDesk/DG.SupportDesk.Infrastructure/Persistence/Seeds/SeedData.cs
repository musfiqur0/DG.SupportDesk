using DG.SupportDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Persistence.Seeds;

public class SeedData
{
    public void Seed(ApplicationDbContext context)
    {
        if (!context.Database.CanConnect())
            return;

        if (context.Database.GetPendingMigrations().Any())
            return;

        SeedTenantTypes(context);
        SeedTicketStatusTypes(context);
    }

    private void SeedTenantTypes(ApplicationDbContext context)
    {
        if (context.TenantTypes.Any())
        {
            return;
        }

        var tenantTypes = new List<TenantType>
        {
            new TenantType
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Internal",
                Code = "INTERNAL",
                Description = "Internal tenant type",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TenantType
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "External",
                Code = "EXTERNAL",
                Description = "External tenant type",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            }
        };


        context.TenantTypes.AddRange(tenantTypes);
        context.SaveChanges();
    }

    private void SeedTicketStatusTypes(ApplicationDbContext context)
    {
        if (context.TicketStatusTypes.Any())
        {
            return;
        }

        var ticketStatusTypes = new List<TicketStatusType>
        {
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333301"),
                Name = "Submitted",
                Code = "SUBMITTED",
                Description = "Ticket has been submitted and is awaiting initial review.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333302"),
                Name = "Administration Review",
                Code = "ADMIN_REVIEW",
                Description = "Ticket is currently under administration review.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333303"),
                Name = "Support Review",
                Code = "SUPPORT_REVIEW",
                Description = "Ticket is currently under support team review.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333304"),
                Name = "In Progress",
                Code = "IN_PROGRESS",
                Description = "Ticket is actively being worked on by the support team.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333305"),
                Name = "Waiting For Client",
                Code = "WAITING_FOR_CLIENT",
                Description = "Ticket is on hold waiting for a response or information from the client.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333306"),
                Name = "Resolved",
                Code = "RESOLVED",
                Description = "Ticket issue has been resolved and is awaiting client confirmation.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333307"),
                Name = "Closed",
                Code = "CLOSED",
                Description = "Ticket has been successfully closed.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333308"),
                Name = "Reopened",
                Code = "REOPENED",
                Description = "Ticket was previously closed but has been reopened.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            },
            new TicketStatusType
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333309"),
                Name = "Cancelled",
                Code = "CANCELLED",
                Description = "Ticket has been cancelled and will not be processed further.",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                CreatedBy = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                IsActive = true
            }
        };

        context.TicketStatusTypes.AddRange(ticketStatusTypes);
        context.SaveChanges();
    }
}