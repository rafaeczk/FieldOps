namespace FieldOps.Modules.Files.Core.Services;

internal interface IFileStorageService
{
    Task<string> UploadAsync(Stream stream, string storageKey, string contentType, CancellationToken ct = default);
    Task DeleteAsync(string storageKey, CancellationToken ct = default);
    string GetPresignedUrl(string storageKey, DateTime expiration);
}
