using AutoMapper;
using Microsoft.Data.Sqlite;
using StarLs.Core.Exceptions;
using System.Text.Json;

namespace StarLs.Application.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
        => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }

    }
    private static Task HandleExceptionAsync(
        HttpContext context,
        Exception exception)
    {
        var status = GetStatusCode(exception);
        string? stackTrace = exception.StackTrace;
        string message = exception.Message;

        var result = JsonSerializer.Serialize(
            new { status, message, stackTrace });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = status;

        return context.Response.WriteAsync(result);
    }

    private static int GetStatusCode(Exception exception)
        => exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            AutoMapperMappingException => StatusCodes.Status501NotImplemented,
            DatabaseException => StatusCodes.Status503ServiceUnavailable,
            _ => StatusCodes.Status500InternalServerError,
        };
}
