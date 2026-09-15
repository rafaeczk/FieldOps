using Amazon.S3;
using Amazon.S3.Model;
using FieldOps.Shared.Infrastructure.S3;

namespace FieldOps.Modules.Files.Core.Services;

internal class FileStorageService(IAmazonS3 s3, S3Options s3Options) : IFileStorageService
{
    private readonly IAmazonS3 s3 = s3;
    private readonly S3Options s3Options = s3Options;

    public async Task<string> UploadAsync(Stream stream, string storageKey, string contentType, CancellationToken ct = default)
    {
        var request = new PutObjectRequest
        {
            BucketName = s3Options.BucketName,
            Key = storageKey,
            InputStream = stream,
            ContentType = contentType
        };

        await s3.PutObjectAsync(request, ct);

        return storageKey;
    }

    public async Task DeleteAsync(string storageKey, CancellationToken ct = default)
    {
        var request = new DeleteObjectRequest
        {
            BucketName = s3Options.BucketName,
            Key = storageKey
        };

        await s3.DeleteObjectAsync(request, ct);
    }

    public string GetPresignedUrl(string storageKey, DateTime expiration)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = s3Options.BucketName,
            Key = storageKey,
            Expires = expiration
        };

        return s3.GetPreSignedURL(request);
    }
}
