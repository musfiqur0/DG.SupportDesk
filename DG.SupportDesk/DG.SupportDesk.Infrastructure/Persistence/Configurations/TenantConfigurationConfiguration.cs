using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DG.SupportDesk.Infrastructure.Persistence.Configurations;

public class TenantConfigurationConfiguration : IEntityTypeConfiguration<Domain.Models.TenantConfiguration>
{
    public void Configure(EntityTypeBuilder<Domain.Models.TenantConfiguration> builder)
    {
        builder.Property(x => x.ConfigurationType)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.ConfigurationJson)
            .HasColumnType("jsonb")
            .IsRequired();
    }
}