using FieldOps.Modules.Files.Core.DTOs;
using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Files.Core.Services;

public interface IFileService
{
    Task<FileDto?> GetFileAsync(FileId fileId);
    Task<Guid> UploadFileAsync(Stream stream, string fileName, string contentType, long fileSize);
    Task DeleteFileAsync(FileId fileId);
}
