using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foody.Data.Migrations
{
    public partial class FixRejectedOnInArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RejectedByUserId",
                table: "Articles",
                newName: "RejectedByUser");

            migrationBuilder.AddColumn<DateTime>(
                name: "RejectedOn",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectedOn",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "RejectedByUser",
                table: "Articles",
                newName: "RejectedByUserId");
        }
    }
}
