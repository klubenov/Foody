using Microsoft.EntityFrameworkCore.Migrations;

namespace Foody.Data.Migrations
{
    public partial class CaloriePerGramInMacros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaloricContentPer100Grams",
                table: "MacroElements",
                newName: "CaloricContentPerGram");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CaloricContentPerGram",
                table: "MacroElements",
                newName: "CaloricContentPer100Grams");
        }
    }
}
