using FieldOps.Modules.Technicians.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Technicians.Core.DAL.Configurations
{
    internal class TechnicianConfiguration : IEntityTypeConfiguration<Technician>
    {
        public void Configure(EntityTypeBuilder<Technician> builder)
        {
            builder.HasKey(o => o.Id);

            builder.HasIndex(o => o.AccountId).IsUnique();

            builder.Property(o => o.FullName).IsRequired();

            builder.Property(a => a.CreatedAt).IsRequired();

            builder.Property(a => a.UpdatedAt).IsRequired();

        }
    }
}
