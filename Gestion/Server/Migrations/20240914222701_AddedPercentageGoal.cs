using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gestion.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddedPercentageGoal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Percentage",
                table: "Goals",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percentage",
                table: "Goals");
        }
    }
}
