using Microsoft.EntityFrameworkCore.Migrations;

namespace Foody.Data.Migrations
{
    public partial class AddedRejectCommentToArticle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RejectComment",
                table: "Articles",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RejectComment",
                table: "Articles");
        }
    }
}
