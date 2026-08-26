using FieldOps.Modules.Technicians.Core.Services;
using FieldOps.Modules.Technicians.Core.DAL;
using FieldOps.Modules.Technicians.Core.DAL.Repositories;
using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Technicians.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgres<TechniciansDbContext>();
            services.AddScoped<ITechnicianUnitOfWork, TechnicianUnitOfWork>();

            services.AddScoped<ITechnicianRepository, TechnicianRepository>();
            services.AddScoped<ITechnicianService, TechnicianService>();
            return services;
        }
    }
}
