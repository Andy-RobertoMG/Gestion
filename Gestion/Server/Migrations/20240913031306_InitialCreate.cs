using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CompletedTasks = table.Column<int>(type: "int", nullable: false),
                    TotalTasks = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Goals__8A4FFFD1913D513C", x => x.GoalId);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalId = table.Column<int>(type: "int", nullable: true),
                    Assignment = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    AssignmentStatus = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    IsImportant = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Assignme__32499E77B8AF2C1B", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK__Assignmen__GoalI__45F365D3",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "GoalId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_GoalId",
                table: "Assignments",
                column: "GoalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Goals");
        }
    }
}
