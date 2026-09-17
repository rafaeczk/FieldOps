using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Reports1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                schema: "reports",
                table: "Reports",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                schema: "reports",
                table: "Reports",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SignatureFileId",
                schema: "reports",
                table: "Reports",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                schema: "reports",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "Longitude",
                schema: "reports",
                table: "Reports");

            migrationBuilder.DropColumn(
                name: "SignatureFileId",
                schema: "reports",
                table: "Reports");
        }
    }
}
