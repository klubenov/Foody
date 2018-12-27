using Microsoft.EntityFrameworkCore.Migrations;

namespace Foody.Data.Migrations
{
    public partial class AddMicroElementTypeBool : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMineral",
                table: "MicroElements",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOther",
                table: "MicroElements",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsVitamin",
                table: "MicroElements",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMineral",
                table: "MicroElements");

            migrationBuilder.DropColumn(
                name: "IsOther",
                table: "MicroElements");

            migrationBuilder.DropColumn(
                name: "IsVitamin",
                table: "MicroElements");
        }
    }
}
