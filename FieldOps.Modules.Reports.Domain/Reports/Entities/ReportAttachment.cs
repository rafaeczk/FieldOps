using FieldOps.Shared.Abstractions.Kernel.Ids;

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
