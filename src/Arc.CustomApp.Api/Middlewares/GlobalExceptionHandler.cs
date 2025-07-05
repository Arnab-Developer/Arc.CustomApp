using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Arc.CustomApp.Api.Middlewares;

/// <summary>If there are any unhandled exceptions in the API endpoints,
/// then it handles those exceptions.</summary>
/// <param name="logger">The logger to log the exception details.</param>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger = logger;

    /// <summary>It handles the global exceptions.</summary>
    /// <param name="httpContext">The http context.</param>
    /// <param name="exception">The exception to handle.</param>
    /// <param name="cancellationToken">A token that can be used to cancel the 
    /// operation before completion.</param>
    /// <returns>True if handled otherwise false.</returns>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogException(exception.Message);

        var problemDetails = new ProblemDetails()
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError
        };

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }
}

internal static partial class LoggerExtensions
{
    [LoggerMessage(EventId = 1, Level = LogLevel.Error, Message = "Error message: {errorMessage}")]
    public static partial void LogException(this ILogger<GlobalExceptionHandler> logger,
        string errorMessage);
}