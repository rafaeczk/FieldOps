using FieldOps.Shared.Abstractions.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Reports.Exceptions
{
    public class FileAlreadyExists(Guid jobId, Guid fileId)
        : BaseException($"File with id: '{fileId}' already exists in job with id: '{jobId}'.")
    {
        public Guid JobId => jobId;
        public Guid FileId => fileId;
    }
}
