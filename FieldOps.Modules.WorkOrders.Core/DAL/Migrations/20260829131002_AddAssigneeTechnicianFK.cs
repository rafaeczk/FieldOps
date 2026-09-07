using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.WorkOrders.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddAssigneeTechnicianFK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                ALTER TABLE workorders."WorkOrderAssignees"
                ADD CONSTRAINT "FK_WorkOrderAssignees_Technicians_TechnicianId"
                FOREIGN KEY ("TechnicianId")
                REFERENCES technicians."Technicians" ("Id")
                ON DELETE CASCADE
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                ALTER TABLE workorders."WorkOrderAssignees"
                DROP CONSTRAINT "FK_WorkOrderAssignees_Technicians_TechnicianId"
                """);
        }
    }
}
