using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.WorkOrders.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixWorkOrderAssigneeFKToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                ALTER TABLE workorders."WorkOrderAssignees"
                DROP CONSTRAINT "FK_WorkOrderAssignees_Technicians_TechnicianId"
                """);

            migrationBuilder.Sql(
                """
                ALTER TABLE workorders."WorkOrderAssignees"
                ADD CONSTRAINT "FK_WorkOrderAssignees_Accounts_TechnicianId"
                FOREIGN KEY ("TechnicianId")
                REFERENCES accounts."Accounts" ("Id")
                ON DELETE CASCADE
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                """
                ALTER TABLE workorders."WorkOrderAssignees"
                DROP CONSTRAINT "FK_WorkOrderAssignees_Accounts_TechnicianId"
                """);

            migrationBuilder.Sql(
                """
                ALTER TABLE workorders."WorkOrderAssignees"
                ADD CONSTRAINT "FK_WorkOrderAssignees_Technicians_TechnicianId"
                FOREIGN KEY ("TechnicianId")
                REFERENCES technicians."Technicians" ("Id")
                ON DELETE CASCADE
                """);
        }
    }
}
