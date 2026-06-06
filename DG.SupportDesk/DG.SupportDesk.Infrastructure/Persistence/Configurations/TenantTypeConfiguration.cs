using DG.SupportDesk.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DG.SupportDesk.Infrastructure.Persistence.Configurations;

public class TenantTypeConfiguration : IEntityTypeConfiguration<TenantType>
{
    public void Configure(EntityTypeBuilder<TenantType> builder)
    {
        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Code)
            .HasMaxLength(50)
            .IsRequired();

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

    }
}
