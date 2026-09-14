using FieldOps.Modules.Assets.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]
namespace FieldOps.Modules.Assets.Api
{

    internal static class Extensions
    {
        public static IServiceCollection AddAssetsModule(this IServiceCollection services)
        {
            services.AddCore();

            return services;
        }

        public static WebApplication UseAssetsModule(this WebApplication app)
        {
            return app;
        }
    }
}

