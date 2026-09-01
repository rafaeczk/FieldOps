using FieldOps.Shared.Abstractions.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions
{
    [Serializable]
    public class ReportNotFoundException(Guid reportId) : BaseException($"Report with ID '{reportId}' was not found.")
    {

    }
}
