using FieldOps.Modules.Technicians.Core.Entities;
using FieldOps.Modules.Technicians.Core.Repositories;
using FieldOps.Shared.Abstractions.Time;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Technicians.Core.DAL.Repositories
{
    internal class TechnicianRepository(TechniciansDbContext context) : ITechnicianRepository
    {
        private readonly TechniciansDbContext context = context;

        public async Task<IReadOnlyList<Technician>> BrowseAsync()
        {
            return await context.Technicians.ToListAsync();
        }

        public async Task CreateAsync(Technician technician)
        {
            context.Technicians.Add(technician);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Technician technician)
        {
            context.Technicians.Remove(technician);
            await context.SaveChangesAsync();
        }
        public async Task<Technician?> GetAsync(Guid id)
        {
            return await context.Technicians.SingleOrDefaultAsync(t => t.Id == id);
        }
    }
}
