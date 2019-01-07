using Microsoft.EntityFrameworkCore.Migrations;

namespace Foody.Data.Migrations
{
    public partial class AddCaloriesFromPropertiesInMeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CaloriesFromCarbohydrates",
                table: "Meals",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CaloriesFromFats",
                table: "Meals",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CaloriesFromProteins",
                table: "Meals",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaloriesFromCarbohydrates",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "CaloriesFromFats",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "CaloriesFromProteins",
                table: "Meals");
        }
    }
}
