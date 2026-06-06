using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DG.SupportDesk.Domain.Models;

namespace DG.SupportDesk.Infrastructure.Persistence.Configurations;

public class TenantConfiguration : IEntityTypeConfiguration<Tenant>
{
    public void Configure(EntityTypeBuilder<Tenant> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();
        
        builder.HasIndex(x => x.Code)
            .IsUnique();

        //builder.Property(x => x.TenantTypeId).HasColumnType("uuid");
    }
}