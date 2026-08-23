using FieldOps.Modules.Technicians.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("FieldOps.Bootstrapper")]

namespace FieldOps.Modules.Technicians.Api
{
    internal static class Extensions
    {
        public static IServiceCollection AddTechniciansModule(this IServiceCollection services)
        {
            services.AddCore();

            return services;
        }

        public static WebApplication UseTechniciansModule(this WebApplication app)
        {
            return app;
        }
    }
}
