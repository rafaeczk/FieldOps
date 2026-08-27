using FieldOps.Modules.Files.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace FieldOps.Modules.Files.Core.DAL;

internal class FilesDbContext(DbContextOptions<FilesDbContext> options) : DbContext(options)
{
    public DbSet<StoredFile> Files { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("files");
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
