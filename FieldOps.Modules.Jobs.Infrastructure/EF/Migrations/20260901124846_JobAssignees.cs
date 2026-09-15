using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class JobAssignees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobAssignees",
                schema: "jobs",
                columns: table => new
                {
                    TechnicianId = table.Column<Guid>(type: "uuid", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobAssignees", x => new { x.JobId, x.TechnicianId });
                    table.ForeignKey(
                        name: "FK_JobAssignees_Jobs_JobId",
                        column: x => x.JobId,
                        principalSchema: "jobs",
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobAssignees",
                schema: "jobs");
        }
    }
}
