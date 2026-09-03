using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Files.Core.Entities;

internal class StoredFile
{
    public FileId Id { get; private set; } = null!;
    public string FileName { get; private set; } = null!;
    public string ContentType { get; set; } = null!;
    public long Size { get; set; }
    public string StorageKey { get; set; } = null!;
    public DateTime CreatedAt { get; set; }

    private StoredFile() { }

    public static StoredFile Create(string fileName, string contentType, long size, string storageKey, DateTime createdAt)
    {
        return new StoredFile
        {
            Id = Guid.NewGuid(),
            FileName = fileName,
            ContentType = contentType,
            Size = size,
            StorageKey = storageKey,
            CreatedAt = createdAt
        };
    }
}
