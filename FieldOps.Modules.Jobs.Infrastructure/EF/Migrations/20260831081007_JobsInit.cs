using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FieldOps.Modules.Jobs.Infrastructure.EF.Migrations
{
    /// <inheritdoc />
    public partial class JobsInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "jobs");

            migrationBuilder.CreateTable(
                name: "Jobs",
                schema: "jobs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Priority = table.Column<string>(type: "text", nullable: false),
                    Address_CountryCode = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    Address_PostalCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address_City = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address_Street = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Address_BuildingNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Address_ApartmentNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Deadline = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobs",
                schema: "jobs");
        }
    }
}
