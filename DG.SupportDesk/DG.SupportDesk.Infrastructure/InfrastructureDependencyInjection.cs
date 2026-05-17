using DG.SupportDesk.Application.Abstractions.Repositories.Support;
using DG.SupportDesk.Infrastructure.Persistence;
using DG.SupportDesk.Infrastructure.Repositories;
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
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<ITenantRepository, TenantRepository>();
        services.AddScoped<IProductProjectRepository, ProductProjectRepository>();
        services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
        services.AddScoped<ISupportTicketCommentRepository, SupportTicketCommentRepository>();
        services.AddScoped<ISupportTicketAttachmentRepository, SupportTicketAttachmentRepository>();
        services.AddScoped<ISupportTicketStatusHistoryRepository, SupportTicketStatusHistoryRepository>();

        return services;
    }
}