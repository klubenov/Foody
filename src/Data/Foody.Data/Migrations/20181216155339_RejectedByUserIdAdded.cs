using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foody.Data.Migrations
{
    public partial class RejectedByUserIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedOn",
                table: "Articles",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RejectedByUserId",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedOn",
                table: "Articles");

            migrationBuilder.DropColumn(
                name: "RejectedByUserId",
                table: "Articles");
        }
    }
}
