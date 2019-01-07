using Microsoft.EntityFrameworkCore.Migrations;

namespace Foody.Data.Migrations
{
    public partial class AddTotalCaloriesInMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalCalories",
                table: "Meals",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalCalories",
                table: "Meals");
        }
    }
}
