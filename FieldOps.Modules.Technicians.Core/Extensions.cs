using FieldOps.Modules.Technicians.Core.Services;
using FieldOps.Modules.Technicians.Core.DAL;
using FieldOps.Modules.Technicians.Core.DAL.Repositories;
using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Abstractions.Events;
using FieldOps.Shared.Infrastructure.Messaging;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace FieldOps.Modules.Technicians.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgres<TechniciansDbContext>();
            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            services.AddScoped<ITechnicianService, TechnicianService>();
            return services;
        }
    }
}
