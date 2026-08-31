using FieldOps.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Shared.Abstractions.Kernel.Ids
{
    public class ReportId(Guid value) : TypeId(value)
    {
    }
}
