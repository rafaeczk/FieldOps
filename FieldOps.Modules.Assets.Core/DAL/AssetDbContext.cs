using FieldOps.Modules.Assets.Core.Entities;
using FieldOps.Shared.Abstractions.Kernel.Ids;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Assets.Core.DAL
{
    internal class AssetDbContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("assets");
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}
