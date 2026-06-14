namespace DG.SupportDesk.Api.Middlewares;

public class ClientInfoLogEnricherMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ClientInfoLogEnricherMiddleware> _logger; // Inject generic ILogger

    public ClientInfoLogEnricherMiddleware(RequestDelegate next, ILogger<ClientInfoLogEnricherMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // 1. Safely extract Client Info (Null-safe)
        var clientIp = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        var clientPort = context.Connection.RemotePort > 0
            ? context.Connection.RemotePort.ToString()
            : "unknown";

        var userAgent = context.Request.Headers.UserAgent.ToString();
        if (string.IsNullOrWhiteSpace(userAgent)) userAgent = "unknown";

        var requestedHost = context.Request.Host.Host;
        if (string.IsNullOrWhiteSpace(requestedHost)) requestedHost = "unknown";

        var correlationId = context.TraceIdentifier;
        if (string.IsNullOrWhiteSpace(correlationId)) correlationId = Guid.NewGuid().ToString();

        // 2. Use native .NET BeginScope instead of Serilog's LogContext
        using (_logger.BeginScope(new Dictionary<string, object>
        {
            ["ClientIp"] = clientIp,
            ["ClientPort"] = clientPort,
            ["UserAgent"] = userAgent,
            ["RequestedHost"] = requestedHost,
            ["CorrelationId"] = correlationId
        }))
        {
            await _next(context);
        }
    }
}