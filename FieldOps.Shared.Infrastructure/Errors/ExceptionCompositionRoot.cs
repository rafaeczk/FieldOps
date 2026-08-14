using Microsoft.Extensions.DependencyInjection;
using FieldOps.Shared.Abstractions.Errors;
using System.Net;

namespace FieldOps.Shared.Infrastructure.Errors;

internal class ExceptionCompositionRoot(IServiceProvider serviceProvider) : IExceptionCompositionRoot
{
    private readonly IServiceProvider serviceProvider = serviceProvider;

    public ErrorResponse Map(Exception exception)
    {
        using var scope = serviceProvider.CreateScope();

        var mappers = scope.ServiceProvider.GetServices<IExceptionToResponseMapper>();

        var customMappers = mappers.Where(m => m is not ExceptionToResponseMapper);
        var result = customMappers.Select(m => m.Map(exception)).SingleOrDefault(r => r is not null);

        if (result is not null) return result;

        var mainMapper = mappers.SingleOrDefault(m => m is ExceptionToResponseMapper);
        var result2 = mainMapper?.Map(exception);

        if (result2 is not null) return result2;

        return new(new { }, HttpStatusCode.InternalServerError);
    }
}
