using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text.Json;

namespace DG.SupportDesk.Api.Middlewares;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

    public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Log the actual exception details to your console/file logs
            _logger.LogError(ex, "Unhandled exception occurred: {Message}", ex.Message);

            // Return a safe, formatted response to the client
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "An unexpected error occurred. Please try again later.";
        object errors = new Dictionary<string, string[]>
        {
            { "ServerError", new[] { "An unexpected error occurred." } }
        };

        switch (exception)
        {
            // 1. FluentValidation Errors (Matches your existing ValidationBadRequest format)
            case ValidationException vex:
                statusCode = HttpStatusCode.BadRequest;
                message = "Validation failed. Please check the submitted data.";
                errors = vex.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorMessage).Distinct().ToArray());
                break;

            // 2. Standard .NET Exceptions
            case UnauthorizedAccessException:
                statusCode = HttpStatusCode.Unauthorized;
                message = exception.Message;
                errors = new Dictionary<string, string[]> { { "Unauthorized", new[] { exception.Message } } };
                break;

            case ArgumentNullException anex:
                statusCode = HttpStatusCode.BadRequest;
                message = anex.Message;
                errors = new Dictionary<string, string[]> { { anex.ParamName ?? "ArgumentNull", new[] { anex.Message } } };
                break;

            case ArgumentException aex:
                statusCode = HttpStatusCode.BadRequest;
                message = aex.Message;
                errors = new Dictionary<string, string[]> { { aex.ParamName ?? "InvalidArgument", new[] { aex.Message } } };
                break;

            case KeyNotFoundException:
                statusCode = HttpStatusCode.NotFound;
                message = exception.Message;
                errors = new Dictionary<string, string[]> { { "NotFound", new[] { exception.Message } } };
                break;

            case InvalidOperationException:
                statusCode = HttpStatusCode.Conflict;
                message = exception.Message;
                errors = new Dictionary<string, string[]> { { "InvalidOperation", new[] { exception.Message } } };
                break;

            // 3. Database Exceptions (EF Core)
            case DbUpdateException dbEx:
                var innerMsg = dbEx.InnerException?.Message ?? string.Empty;

                // Handle Unique Constraint Violations
                if (innerMsg.Contains("duplicate key") || innerMsg.Contains("unique constraint"))
                {
                    statusCode = HttpStatusCode.Conflict;
                    message = "A record with this unique value already exists.";
                    errors = new Dictionary<string, string[]> { { "UniqueConstraintViolation", new[] { message } } };
                }
                // Handle the specific JSON parsing error you encountered earlier
                else if (innerMsg.Contains("invalid input syntax for type json"))
                {
                    statusCode = HttpStatusCode.BadRequest;
                    message = "The provided Configuration JSON is not in a valid format.";
                    errors = new Dictionary<string, string[]> { { "InvalidJsonFormat", new[] { message } } };
                }
                // Fallback for other DB errors
                else
                {
                    message = "A database error occurred while saving changes.";
                    errors = new Dictionary<string, string[]> { { "DatabaseError", new[] { message } } };
                }
                break;
        }

        context.Response.StatusCode = (int)statusCode;

        // Format matches your existing ValidationBadRequest response structure
        var response = new
        {
            Success = false,
            Message = message,
            Errors = errors,
            TraceId = context.TraceIdentifier
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return context.Response.WriteAsync(json);
    }
}