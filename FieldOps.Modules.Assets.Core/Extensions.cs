using FieldOps.Modules.Assets.Contracts;
using FieldOps.Modules.Assets.Core.DAL;
using FieldOps.Modules.Assets.Core.DAL.Repositories;
using FieldOps.Modules.Assets.Core.Repositories;
using FieldOps.Modules.Assets.Core.Services;
using FieldOps.Shared.Infrastructure.Events;
using FieldOps.Shared.Infrastructure.Messages;
using FieldOps.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddPostgres<AssetsDbContext>();
            services.AddScoped<IAssetRepository, AssetRepository>();

            services.AddMediatRNotificationHandlers(typeof(ModuleMarker));
            services.AddMediatRRequestHandlers(typeof(ModuleMarker));

            services.AddScoped<IAssetService, AssetService>();
            services.AddScoped<IAssetsModuleApi, AssetsModuleApi>();

            services.AddScoped<IAssetUnitOfWork, AssetUnitOfWork>();


            return services;
        }
    }
    internal class ModuleMarker { }
}
