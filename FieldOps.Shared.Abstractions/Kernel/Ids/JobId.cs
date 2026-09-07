using FieldOps.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Shared.Abstractions.Kernel.Ids;

public class JobId(Guid value) : TypeId(value)
{
    public static implicit operator JobId(Guid id) => new(id);
    public static implicit operator Guid(JobId id) => id.Value;
}
