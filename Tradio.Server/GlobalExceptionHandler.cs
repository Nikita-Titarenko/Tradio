using System.Text.Json;
using FluentResults;

namespace Tradio.Server;

public class GlobalExceptionHandler(RequestDelegate next, ILogger<GlobalExceptionHandler> logger)
{
    private const string _errorMessage = "An unexpected error occurred.";
    private readonly RequestDelegate _next = next;
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, _errorMessage);

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var errorResponse = Result.Fail(new Error(_errorMessage).WithMetadata("Code", "UnexpectedError"));

            var jsonResponse = JsonSerializer.Serialize(errorResponse);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
