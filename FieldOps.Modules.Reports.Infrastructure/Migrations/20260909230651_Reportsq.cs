using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reportsq : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RaportAttachments",
                schema: "reports");

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                schema: "reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProcessedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OutboxMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReportAttachments",
                schema: "reports",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReportAttachments", x => new { x.ReportId, x.FileId });
                    table.ForeignKey(
                        name: "FK_ReportAttachments_Reports_ReportId",
                        column: x => x.ReportId,
                        principalSchema: "reports",
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OutboxMessages",
                schema: "reports");

            migrationBuilder.DropTable(
                name: "ReportAttachments",
                schema: "reports");

            migrationBuilder.CreateTable(
                name: "RaportAttachments",
                schema: "reports",
                columns: table => new
                {
                    RaportId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaportAttachments", x => new { x.RaportId, x.FileId });
                    table.ForeignKey(
                        name: "FK_RaportAttachments_Reports_RaportId",
                        column: x => x.RaportId,
                        principalSchema: "reports",
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }
    }
}
