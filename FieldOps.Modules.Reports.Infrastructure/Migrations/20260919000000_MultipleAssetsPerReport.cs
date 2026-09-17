using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MultipleAssetsPerReport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                CREATE TABLE reports.""ReportAssets"" (
                    ""ReportId"" uuid NOT NULL,
                    ""AssetId"" uuid NOT NULL,
                    CONSTRAINT ""PK_ReportAssets"" PRIMARY KEY (""ReportId"", ""AssetId""),
                    CONSTRAINT ""FK_ReportAssets_Reports_ReportId"" FOREIGN KEY (""ReportId"") REFERENCES reports.""Reports"" (""Id"") ON DELETE CASCADE
                );

                INSERT INTO reports.""ReportAssets"" (""ReportId"", ""AssetId"")
                SELECT ""Id"", ""AssetId""
                FROM reports.""Reports""
                WHERE ""AssetId"" IS NOT NULL;
            ");

            migrationBuilder.DropColumn(
                name: "AssetId",
                schema: "reports",
                table: "Reports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                schema: "reports",
                table: "Reports",
                type: "uuid",
                nullable: true);

            migrationBuilder.Sql(@"
                UPDATE reports.""Reports"" r
                SET ""AssetId"" = ra.""AssetId""
                FROM reports.""ReportAssets"" ra
                WHERE r.""Id"" = ra.""ReportId"";
            ");

            migrationBuilder.DropTable(
                name: "ReportAssets",
                schema: "reports");
        }
    }
}
