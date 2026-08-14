using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Shared.Infrastructure.Api;

public static class ControllerExtensions
{
    public static ActionResult<T> OkOrNotFound<C, T>(this C controller, T model)
        where C : ControllerBase
    {
        if (model is null)
        {
            return controller.NotFound();
        }

        return controller.Ok(model);
    }
}
