using FieldOps.Modules.Files.Core.DTOs;

namespace FieldOps.Modules.Files.Core.Services;

public interface IFileService
{
    Task<FileDto?> GetFileAsync(Guid fileId);
    Task<Guid> UploadFileAsync(Stream stream, string fileName, string contentType, long fileSize);
    Task DeleteFileAsync(Guid fileId);
}
