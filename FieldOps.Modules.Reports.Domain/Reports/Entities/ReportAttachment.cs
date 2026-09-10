using FieldOps.Shared.Abstractions.Kernel.Ids;
using FieldOps.Shared.Abstractions.Kernel.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace FieldOps.Modules.Reports.Domain.Reports.Entities
{
    public sealed class ReportAttachment
    {
        public ReportId ReportId { get; set; } = null!;
        public FileId FileId { get; set; } = null!;

        private ReportAttachment() { }

        public static ReportAttachment Create(FileId fileId, ReportId reportId)
        {
            return new()
            {
                FileId = fileId,
                ReportId = reportId
            };
        }
    }


}
