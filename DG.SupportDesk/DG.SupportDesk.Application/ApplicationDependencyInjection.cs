using Microsoft.Extensions.DependencyInjection;

namespace DG.SupportDesk.Application;

public static class ApplicationDependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        //services.AddScoped<ITenantService, TenantService>();
        //services.AddScoped<ITenantTypeService, TenantTypeService>();
        //services.AddScoped<ISupportTicketService, SupportTicketService>();

        return services;
    }
}
