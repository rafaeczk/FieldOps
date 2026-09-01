using FieldOps.Modules.Reports.Domain.Reports.Entities;
using FieldOps.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Reports.Repositories
{
    public interface IReportsWriteRepository
    {
        void Add(Report report);
        void Update(Report report);
    }
}
