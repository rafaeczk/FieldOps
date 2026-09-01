using FieldOps.Shared.Abstractions.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions
{
    [Serializable]
    public class FileDoesNotExistException() : BaseException($"One or more files do not exist.")
    {
    }
}
