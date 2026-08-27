using FieldOps.Modules.Files.Core.DTOs;
using FieldOps.Modules.Files.Core.Exceptions;
using FieldOps.Modules.Files.Core.Services;
using FieldOps.Shared.Infrastructure.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FieldOps.Modules.Files.Api.Controllers;

internal class FilesController(IFileService service) : BaseController
{
    [HttpGet("{fileId}")]
    public async Task<ActionResult<FileDto>> GetFile(Guid fileId)
    {
        return this.OkOrNotFound(await service.GetFileAsync(fileId));
    }

    [HttpPost("upload")]
    public async Task<ActionResult<Guid>> UploadFile(IFormFile file)
    {
        if (file is null || file.Length is 0)
            throw new EmptyOrMissingFileException();

        using var stream = file.OpenReadStream();

        Guid fileId = await service.UploadFileAsync(stream, file.FileName, file.ContentType, file.Length);

        return Ok(fileId);
    }

    [HttpDelete("{fileId}")]
    public async Task<ActionResult> DeleteFile(Guid fileId)
    {
        await service.DeleteFileAsync(fileId);
        return NoContent();
    }
}
