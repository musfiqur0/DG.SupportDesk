using Serilog;

namespace DG.SupportDesk.Api.Extensions;

public static class SerilogExtension
{
    public static void SerilogConfiguration(this IHostBuilder builder)
    {
        builder.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
}
