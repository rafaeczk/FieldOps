using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FieldOps.Shared.Infrastructure.Errors;

internal class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger, IExceptionCompositionRoot exceptionCompositionRoot) : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> logger = logger;
    private readonly IExceptionCompositionRoot exceptionCompositionRoot = exceptionCompositionRoot;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            logger.LogError(exception, exception.Message);
            await HandleError(context, exception);
        }
    }

    private async Task HandleError(HttpContext context, Exception exception)
    {
        var response = exceptionCompositionRoot.Map(exception);

        context.Response.StatusCode = (int)response.StatusCode;

        await context.Response.WriteAsJsonAsync(response.Response);
    }
}
