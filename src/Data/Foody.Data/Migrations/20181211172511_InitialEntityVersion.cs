using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Foody.Data.Migrations
{
    public partial class InitialEntityVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true),
                    PostDate = table.Column<DateTime>(nullable: false),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsSentForApproval = table.Column<bool>(nullable: false),
                    IsRejected = table.Column<bool>(nullable: false),
                    ImageLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FoodItems",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    FoodyUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_AspNetUsers_FoodyUserId",
                        column: x => x.FoodyUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MacroElements",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageLocation = table.Column<string>(nullable: true),
                    CaloricContentPer100Grams = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MacroElements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MicroElements",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageLocation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MicroElements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ImageLocation = table.Column<string>(nullable: true),
                    IsApproved = table.Column<bool>(nullable: false),
                    IsSentForApproval = table.Column<bool>(nullable: false),
                    IsRejected = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    PostDate = table.Column<DateTime>(nullable: false),
                    ArticleId = table.Column<string>(nullable: true),
                    AuthorId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Comments_AspNetUsers_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Meals",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    TimeOfConsumption = table.Column<DateTime>(nullable: false),
                    FoodyUserId = table.Column<string>(nullable: true),
                    LocationId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meals_AspNetUsers_FoodyUserId",
                        column: x => x.FoodyUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Meals_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FoodItemMacroElements",
                columns: table => new
                {
                    FoodItemId = table.Column<string>(nullable: false),
                    MacroElementId = table.Column<string>(nullable: false),
                    AmountInGrams = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemMacroElements", x => new { x.FoodItemId, x.MacroElementId });
                    table.ForeignKey(
                        name: "FK_FoodItemMacroElements_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItemMacroElements_MacroElements_MacroElementId",
                        column: x => x.MacroElementId,
                        principalTable: "MacroElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodItemMicroElements",
                columns: table => new
                {
                    FoodItemId = table.Column<string>(nullable: false),
                    MicroElementId = table.Column<string>(nullable: false),
                    AmountInMilligrams = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemMicroElements", x => new { x.FoodItemId, x.MicroElementId });
                    table.ForeignKey(
                        name: "FK_FoodItemMicroElements_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodItemMicroElements_MicroElements_MicroElementId",
                        column: x => x.MicroElementId,
                        principalTable: "MicroElements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeFoodItems",
                columns: table => new
                {
                    RecipeId = table.Column<string>(nullable: false),
                    FoodItemId = table.Column<string>(nullable: false),
                    AmountInGrams = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeFoodItems", x => new { x.RecipeId, x.FoodItemId });
                    table.ForeignKey(
                        name: "FK_RecipeFoodItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeFoodItems_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealFoodItems",
                columns: table => new
                {
                    MealId = table.Column<string>(nullable: false),
                    FoodItemId = table.Column<string>(nullable: false),
                    AmountInGrams = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealFoodItems", x => new { x.MealId, x.FoodItemId });
                    table.ForeignKey(
                        name: "FK_MealFoodItems_FoodItems_FoodItemId",
                        column: x => x.FoodItemId,
                        principalTable: "FoodItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealFoodItems_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealRecipes",
                columns: table => new
                {
                    MealId = table.Column<string>(nullable: false),
                    RecipeId = table.Column<string>(nullable: false),
                    AmountInGrams = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealRecipes", x => new { x.MealId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_MealRecipes_Meals_MealId",
                        column: x => x.MealId,
                        principalTable: "Meals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_AuthorId",
                table: "Articles",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ArticleId",
                table: "Comments",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AuthorId",
                table: "Comments",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemMacroElements_MacroElementId",
                table: "FoodItemMacroElements",
                column: "MacroElementId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodItemMicroElements_MicroElementId",
                table: "FoodItemMicroElements",
                column: "MicroElementId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_FoodyUserId",
                table: "Locations",
                column: "FoodyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MealFoodItems_FoodItemId",
                table: "MealFoodItems",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_MealRecipes_RecipeId",
                table: "MealRecipes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_FoodyUserId",
                table: "Meals",
                column: "FoodyUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Meals_LocationId",
                table: "Meals",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeFoodItems_FoodItemId",
                table: "RecipeFoodItems",
                column: "FoodItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_OwnerId",
                table: "Recipes",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FoodItemMacroElements");

            migrationBuilder.DropTable(
                name: "FoodItemMicroElements");

            migrationBuilder.DropTable(
                name: "MealFoodItems");

            migrationBuilder.DropTable(
                name: "MealRecipes");

            migrationBuilder.DropTable(
                name: "RecipeFoodItems");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "MacroElements");

            migrationBuilder.DropTable(
                name: "MicroElements");

            migrationBuilder.DropTable(
                name: "Meals");

            migrationBuilder.DropTable(
                name: "FoodItems");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
