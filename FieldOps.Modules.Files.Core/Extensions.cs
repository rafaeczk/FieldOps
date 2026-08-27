using Amazon.S3;
using FieldOps.Modules.Files.Core.DAL;
using FieldOps.Modules.Files.Core.DAL.Repositories;
using FieldOps.Modules.Files.Core.Repositories;
using FieldOps.Modules.Files.Core.Services;
using FieldOps.Shared.Infrastructure.Postgres;
using FieldOps.Shared.Infrastructure.S3;
using Microsoft.Extensions.DependencyInjection;

namespace FieldOps.Modules.Files.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddPostgres<FilesDbContext>();
        services.AddScoped<IFilesUnitOfWork, FilesUnitOfWork>();

        services.AddScoped<IStoredFilesRepository, StoredFilesRepository>();
        services.AddScoped<IFileService, FileService>();

        services.AddSingleton<IAmazonS3>(sp =>
        {
            var options = sp.GetRequiredService<S3Options>();

            return new AmazonS3Client(
                options.AccessKeyId,
                options.SecretAccessKey,
                new AmazonS3Config
                {
                    ServiceURL = options.ServiceUrl,
                    ForcePathStyle = true
                });
        });
        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }
}

internal class ModuleMarker { }
