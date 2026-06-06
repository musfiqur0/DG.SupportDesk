using DG.SupportDesk.Application.Abstractions.Persistence;
using DG.SupportDesk.Infrastructure.Persistence;
using DG.SupportDesk.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DG.SupportDesk.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")),
            optionsLifetime: ServiceLifetime.Singleton);

        services.AddScoped<ISupportDeskDbContext, ApplicationDbContext>();

        services.AddScoped<SeedData>();
        return services;
    }
}