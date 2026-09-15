using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.Accounts.Core.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddFullNameToAccount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                schema: "accounts",
                table: "Accounts",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                schema: "accounts",
                table: "Accounts");
        }
    }
}
