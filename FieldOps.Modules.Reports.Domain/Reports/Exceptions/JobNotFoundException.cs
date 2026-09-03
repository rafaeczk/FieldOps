using FieldOps.Shared.Abstractions.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions
{
    [Serializable]
    public class JobNotFoundException(Guid jobId) : BaseException($"Job with ID '{jobId}' was not found.")
    {

    }
}
