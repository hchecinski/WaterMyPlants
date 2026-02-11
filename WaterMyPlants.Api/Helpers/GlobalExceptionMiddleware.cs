using Microsoft.AspNetCore.Mvc;
using WaterMyPlants.Domain.Exceptions;

namespace WaterMyPlants.Api.Helpers;

public sealed class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public GlobalExceptionMiddleware(ILogger<GlobalExceptionMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            _logger.LogWarning(ex, ex.Message);

            await WriteProblemDetails(
                context,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Domain rule violation",
                detail: ex.Message);
        }
        catch (NotFoundException ex)
        {
            _logger.LogInformation(ex.Message);
            await WriteProblemDetails(context, 404, "Resource not found", ex.Message);
        }
        catch (ConcurrencyException ex)
        {
            _logger.LogWarning(ex, "Concurrency conflict");
            await WriteProblemDetails(
                context,
                statusCode: StatusCodes.Status409Conflict,
                title: "Concurrency Conflict",
                detail: ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await WriteProblemDetails(
                context,
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Internal Server Error",
                detail: "An unexpected error occurred.");
        }
    }

    private static async Task WriteProblemDetails(HttpContext context, int statusCode, string title, string detail)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail
        };

        await context.Response.WriteAsJsonAsync(problem);
    }
}
