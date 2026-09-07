using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.WorkOrders.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddWorkOrderPriorityAndAssignees : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_TechnicianId",
                schema: "workorders",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "TechnicianId",
                schema: "workorders",
                table: "WorkOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "workorders",
                table: "WorkOrders",
                type: "character varying(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                schema: "workorders",
                table: "WorkOrders",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "WorkOrderAssignees",
                schema: "workorders",
                columns: table => new
                {
                    WorkOrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    TechnicianId = table.Column<Guid>(type: "uuid", nullable: false),
                    AssignedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrderAssignees", x => new { x.WorkOrderId, x.TechnicianId });
                    table.ForeignKey(
                        name: "FK_WorkOrderAssignees_WorkOrders_WorkOrderId",
                        column: x => x.WorkOrderId,
                        principalSchema: "workorders",
                        principalTable: "WorkOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_Priority",
                schema: "workorders",
                table: "WorkOrders",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrderAssignees_TechnicianId",
                schema: "workorders",
                table: "WorkOrderAssignees",
                column: "TechnicianId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkOrderAssignees",
                schema: "workorders");

            migrationBuilder.DropIndex(
                name: "IX_WorkOrders_Priority",
                schema: "workorders",
                table: "WorkOrders");

            migrationBuilder.DropColumn(
                name: "Priority",
                schema: "workorders",
                table: "WorkOrders");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                schema: "workorders",
                table: "WorkOrders",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TechnicianId",
                schema: "workorders",
                table: "WorkOrders",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_TechnicianId",
                schema: "workorders",
                table: "WorkOrders",
                column: "TechnicianId");
        }
    }
}
