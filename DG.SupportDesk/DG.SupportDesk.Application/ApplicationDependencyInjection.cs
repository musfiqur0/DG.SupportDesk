using DG.SupportDesk.Application.Abstractions.Services;
using DG.SupportDesk.Application.Abstractions.Services.Support;
using DG.SupportDesk.Application.Services;
using DG.SupportDesk.Application.Services.Support;
using Microsoft.Extensions.DependencyInjection;

namespace DG.SupportDesk.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITenantService, TenantService>();
        services.AddScoped<IProductProjectService, ProductProjectService>();
        services.AddScoped<ISupportTicketService, SupportTicketService>();

        return services;
    }
}
