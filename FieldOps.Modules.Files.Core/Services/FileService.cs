using FieldOps.Modules.Files.Core.DTOs;
using FieldOps.Modules.Files.Core.Entities;
using FieldOps.Modules.Files.Core.Exceptions;
using FieldOps.Modules.Files.Core.Repositories;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Time;

namespace FieldOps.Modules.Files.Core.Services;

internal class FileService(IFileStorageService storageService, IStoredFilesRepository filesRepository, IFilesUnitOfWork unitOfWork, IClock clock) : IFileService
{
    private readonly IFileStorageService storageService = storageService;
    private readonly IStoredFilesRepository filesRepository = filesRepository;
    private readonly IFilesUnitOfWork unitOfWork = unitOfWork;
    private readonly IClock clock = clock;

    public async Task<Guid> UploadFileAsync(Stream stream, string fileName, string contentType, long fileSize)
    {
        var storageKey = $"{Guid.NewGuid()}_{fileName}";

        var file = StoredFile.Create(fileName, contentType, fileSize, storageKey, clock.UtcNow());

        filesRepository.Add(file);
        await storageService.UploadAsync(stream, storageKey, contentType);

        await unitOfWork.SaveChangesAsync();

        return file.Id;
    }

    public async Task<FileDto?> GetFileAsync(FileId fileId)
    {
        var file = await filesRepository.GetAsync(fileId);

        if (file is null)
            return null;

        var url = storageService.GetPresignedUrl(file.StorageKey, clock.UtcNow().AddDays(1));

        return new(url, file.FileName, file.ContentType);
    }

    public async Task DeleteFileAsync(FileId fileId)
    {
        var file = await filesRepository.GetAsync(fileId);

        if (file is null)
            throw new StoredFileNotFoundException(fileId);

        filesRepository.Delete(file);
        await unitOfWork.SaveChangesAsync();
    }
}
