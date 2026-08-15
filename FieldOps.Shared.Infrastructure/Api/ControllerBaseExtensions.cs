using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Shared.Infrastructure.Api;

public static class ControllerBaseExtensions
{
    public static ActionResult<T> OkOrNotFound<T>(this ControllerBase controller, T? model)
    {
        if (model is null)
        {
            return controller.NotFound();
        }

        return controller.Ok(model);
    }
}
