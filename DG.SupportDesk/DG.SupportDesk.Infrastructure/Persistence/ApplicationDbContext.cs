using DG.SupportDesk.Domain.Models;
using DG.SupportDesk.Domain.Models.Support;
using Microsoft.EntityFrameworkCore;

namespace DG.SupportDesk.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<ProductProject> ProductProjects => Set<ProductProject>();
    public DbSet<SupportTicket> SupportTickets => Set<SupportTicket>();
    public DbSet<SupportTicketComment> SupportTicketComments => Set<SupportTicketComment>();
    public DbSet<SupportTicketAttachment> SupportTicketAttachments => Set<SupportTicketAttachment>();
    public DbSet<SupportTicketStatusHistory> SupportTicketStatusHistories => Set<SupportTicketStatusHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        ConfigureTenant(modelBuilder);
        ConfigureProductProject(modelBuilder);
        ConfigureSupportTicket(modelBuilder);
        ConfigureSupportTicketComment(modelBuilder);
        ConfigureSupportTicketAttachment(modelBuilder);
        ConfigureSupportTicketStatusHistory(modelBuilder);
    }

    private static void ConfigureTenant(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tenant>(entity =>
        {
            //entity.ToTable("Tenants");

            //entity.HasKey(x => x.Id);

            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Code).HasMaxLength(50).IsRequired();

            entity.HasIndex(x => x.Code).IsUnique();
        });
    }

    private static void ConfigureProductProject(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductProject>(entity =>
        {
            //entity.ToTable("ProductProjects");

            //entity.HasKey(x => x.Id);

            entity.Property(x => x.Name).HasMaxLength(200).IsRequired();
            entity.Property(x => x.Code).HasMaxLength(50).IsRequired();
            entity.Property(x => x.Description).HasMaxLength(1000);

            entity.HasIndex(x => new { x.TenantId, x.Code }).IsUnique();

            //entity.HasOne(x => x.Tenant)
            //    .WithMany(x => x.ProductProjects)
            //    .HasForeignKey(x => x.TenantId)
            //    .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureSupportTicket(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportTicket>(entity =>
        {
            //entity.ToTable("SupportTickets");

            //entity.HasKey(x => x.Id);

            entity.Property(x => x.IssueNo)
                .ValueGeneratedOnAdd()
                .UseIdentityByDefaultColumn();

            entity.Property(x => x.TicketCode).HasMaxLength(50);

            entity.Property(x => x.IssueName).HasMaxLength(200).IsRequired();
            entity.Property(x => x.IssueTitle).HasMaxLength(300).IsRequired();
            entity.Property(x => x.IssueDescription).HasMaxLength(5000).IsRequired();

            entity.Property(x => x.IssuerPhoneNo).HasMaxLength(30);
            entity.Property(x => x.IssuerEmail).HasMaxLength(150);

            entity.Property(x => x.ResolverPhoneNo).HasMaxLength(30);
            entity.Property(x => x.ResolverEmail).HasMaxLength(150);

            entity.Property(x => x.Remarks).HasMaxLength(1000);

            entity.Property(x => x.IssueCategoryTypeId).HasConversion<long>();
            entity.Property(x => x.PriorityTypeId).HasConversion<long>();
            entity.Property(x => x.SupportLevelTypeId).HasConversion<long>();
            entity.Property(x => x.TicketStatusTypeId).HasConversion<long>();

            entity.HasIndex(x => new { x.TenantId, x.TicketCode }).IsUnique();
            //entity.HasIndex(x => new { x.TenantId, x.ProductProjectId });
            //entity.HasIndex(x => new { x.TenantId, x.TicketStatusTypeId });
            //entity.HasIndex(x => new { x.TenantId, x.PriorityTypeId });

            //entity.HasOne(x => x.Tenant)
            //    .WithMany()
            //    .HasForeignKey(x => x.TenantId)
            //    .OnDelete(DeleteBehavior.Restrict);

            //entity.HasOne(x => x.ProductProject)
            //    .WithMany(x => x.SupportTickets)
            //    .HasForeignKey(x => x.ProductProjectId)
            //    .OnDelete(DeleteBehavior.Restrict);
        });
    }

    private static void ConfigureSupportTicketComment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportTicketComment>(entity =>
        {
            //entity.ToTable("SupportTicketComments");

            //entity.HasKey(x => x.Id);

            entity.Property(x => x.Comment).HasMaxLength(3000).IsRequired();

            //entity.HasIndex(x => new { x.TenantId, x.SupportTicketId });

            //entity.HasOne(x => x.SupportTicket)
            //    .WithMany(x => x.Comments)
            //    .HasForeignKey(x => x.SupportTicketId)
            //    .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureSupportTicketAttachment(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportTicketAttachment>(entity =>
        {
            //entity.ToTable("SupportTicketAttachments");

            //entity.HasKey(x => x.Id);

            entity.Property(x => x.FileName).HasMaxLength(300).IsRequired();
            entity.Property(x => x.FileUrl).HasMaxLength(1000).IsRequired();
            entity.Property(x => x.ContentType).HasMaxLength(150);

            //entity.HasIndex(x => new { x.TenantId, x.SupportTicketId });

            //entity.HasOne(x => x.SupportTicket)
            //    .WithMany(x => x.Attachments)
            //    .HasForeignKey(x => x.SupportTicketId)
            //    .OnDelete(DeleteBehavior.Cascade);
        });
    }

    private static void ConfigureSupportTicketStatusHistory(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupportTicketStatusHistory>(entity =>
        {
            entity.ToTable("SupportTicketStatusHistories");

            entity.HasKey(x => x.Id);

            entity.Property(x => x.FromTicketStatusTypeId).HasConversion<long?>();
            entity.Property(x => x.ToTicketStatusTypeId).HasConversion<long>();

            entity.Property(x => x.Remarks).HasMaxLength(1000);

            //entity.HasIndex(x => new { x.TenantId, x.SupportTicketId });

            //entity.HasOne(x => x.SupportTicket)
            //    .WithMany(x => x.StatusHistories)
            //    .HasForeignKey(x => x.SupportTicketId)
            //    .OnDelete(DeleteBehavior.Cascade);
        });
    }
}