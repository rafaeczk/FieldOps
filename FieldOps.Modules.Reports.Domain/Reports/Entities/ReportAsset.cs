using FieldOps.Shared.Abstractions.Kernel.Ids;

namespace FieldOps.Modules.Reports.Domain.Reports.Entities
{
    public sealed class ReportAsset
    {
        public ReportId ReportId { get; set; } = null!;
        public AssetId AssetId { get; set; } = null!;

        private ReportAsset() { }

        public static ReportAsset Create(AssetId assetId, ReportId reportId)
        {
            return new()
            {
                AssetId = assetId,
                ReportId = reportId
            };
        }
    }
}
