using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel_Models_Name : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Goals",
                newName: "GoalName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoalName",
                table: "Goals",
                newName: "Name");
        }
    }
}
