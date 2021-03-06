﻿// <auto-generated />
using System;
using Foody.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Foody.Data.Migrations
{
    [DbContext(typeof(FoodyDbContext))]
    partial class FoodyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Foody.Data.Models.FoodDiary.Location", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FoodyUserId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("FoodyUserId");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("Foody.Data.Models.FoodDiary.Meal", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CaloriesFromCarbohydrates");

                    b.Property<double>("CaloriesFromFats");

                    b.Property<double>("CaloriesFromProteins");

                    b.Property<string>("FoodyUserId");

                    b.Property<string>("LocationId");

                    b.Property<string>("Note");

                    b.Property<DateTime>("TimeOfConsumption");

                    b.Property<double>("TotalCalories");

                    b.HasKey("Id");

                    b.HasIndex("FoodyUserId");

                    b.HasIndex("LocationId");

                    b.ToTable("Meals");
                });

            modelBuilder.Entity("Foody.Data.Models.FoodDiary.MealFoodItem", b =>
                {
                    b.Property<string>("MealId");

                    b.Property<string>("FoodItemId");

                    b.Property<double>("AmountInGrams");

                    b.HasKey("MealId", "FoodItemId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("MealFoodItems");
                });

            modelBuilder.Entity("Foody.Data.Models.FoodDiary.MealRecipe", b =>
                {
                    b.Property<string>("MealId");

                    b.Property<string>("RecipeId");

                    b.Property<double>("AmountInGrams");

                    b.HasKey("MealId", "RecipeId");

                    b.HasIndex("RecipeId");

                    b.ToTable("MealRecipes");
                });

            modelBuilder.Entity("Foody.Data.Models.FoodyUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.FoodItem", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageLocation");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("FoodItems");
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.FoodItemMacroElement", b =>
                {
                    b.Property<string>("FoodItemId");

                    b.Property<string>("MacroElementId");

                    b.Property<double>("AmountInGrams");

                    b.HasKey("FoodItemId", "MacroElementId");

                    b.HasIndex("MacroElementId");

                    b.ToTable("FoodItemMacroElements");
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.FoodItemMicroElement", b =>
                {
                    b.Property<string>("FoodItemId");

                    b.Property<string>("MicroElementId");

                    b.Property<double>("AmountInMilligrams");

                    b.HasKey("FoodItemId", "MicroElementId");

                    b.HasIndex("MicroElementId");

                    b.ToTable("FoodItemMicroElements");
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.MacroElement", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("CaloricContentPerGram");

                    b.Property<string>("Description");

                    b.Property<string>("ImageLocation");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("MacroElements");
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.MicroElement", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageLocation");

                    b.Property<bool>("IsMineral");

                    b.Property<bool>("IsOther");

                    b.Property<bool>("IsVitamin");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("MicroElements");
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.Recipe", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("ImageLocation");

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("IsRejected");

                    b.Property<bool>("IsSentForApproval");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("OwnerId");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.HasIndex("OwnerId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.RecipeFoodItem", b =>
                {
                    b.Property<string>("RecipeId");

                    b.Property<string>("FoodItemId");

                    b.Property<double>("AmountInGrams");

                    b.HasKey("RecipeId", "FoodItemId");

                    b.HasIndex("FoodItemId");

                    b.ToTable("RecipeFoodItems");
                });

            modelBuilder.Entity("Foody.Data.Models.Publishing.Article", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("ApprovedOn");

                    b.Property<string>("AuthorId");

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<string>("ImageLocation");

                    b.Property<bool>("IsApproved");

                    b.Property<bool>("IsRejected");

                    b.Property<bool>("IsSentForApproval");

                    b.Property<DateTime>("PostDate");

                    b.Property<string>("RejectComment");

                    b.Property<string>("RejectedByUser");

                    b.Property<DateTime?>("RejectedOn");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("Title")
                        .IsUnique();

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("Foody.Data.Models.Publishing.Comment", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ArticleId");

                    b.Property<string>("AuthorId");

                    b.Property<string>("Content");

                    b.Property<DateTime>("PostDate");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Foody.Data.Models.FoodDiary.Location", b =>
                {
                    b.HasOne("Foody.Data.Models.FoodyUser", "FoodyUser")
                        .WithMany("Locations")
                        .HasForeignKey("FoodyUserId");
                });

            modelBuilder.Entity("Foody.Data.Models.FoodDiary.Meal", b =>
                {
                    b.HasOne("Foody.Data.Models.FoodyUser", "FoodyUser")
                        .WithMany("Meals")
                        .HasForeignKey("FoodyUserId");

                    b.HasOne("Foody.Data.Models.FoodDiary.Location", "Location")
                        .WithMany("Meals")
                        .HasForeignKey("LocationId");
                });

            modelBuilder.Entity("Foody.Data.Models.FoodDiary.MealFoodItem", b =>
                {
                    b.HasOne("Foody.Data.Models.Nutrition.FoodItem", "FoodItem")
                        .WithMany("MealFoodItems")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foody.Data.Models.FoodDiary.Meal", "Meal")
                        .WithMany("MealFoodItems")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Foody.Data.Models.FoodDiary.MealRecipe", b =>
                {
                    b.HasOne("Foody.Data.Models.FoodDiary.Meal", "Meal")
                        .WithMany("MealRecipes")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foody.Data.Models.Nutrition.Recipe", "Recipe")
                        .WithMany("MealRecipes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.FoodItemMacroElement", b =>
                {
                    b.HasOne("Foody.Data.Models.Nutrition.FoodItem", "FoodItem")
                        .WithMany("FoodItemMacroElements")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foody.Data.Models.Nutrition.MacroElement", "MacroElement")
                        .WithMany("FoodItemMacroElements")
                        .HasForeignKey("MacroElementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.FoodItemMicroElement", b =>
                {
                    b.HasOne("Foody.Data.Models.Nutrition.FoodItem", "FoodItem")
                        .WithMany("FoodItemMicroElements")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foody.Data.Models.Nutrition.MicroElement", "MicroElement")
                        .WithMany("FoodItemMicroElements")
                        .HasForeignKey("MicroElementId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.Recipe", b =>
                {
                    b.HasOne("Foody.Data.Models.FoodyUser", "Owner")
                        .WithMany("Recipes")
                        .HasForeignKey("OwnerId");
                });

            modelBuilder.Entity("Foody.Data.Models.Nutrition.RecipeFoodItem", b =>
                {
                    b.HasOne("Foody.Data.Models.Nutrition.FoodItem", "FoodItem")
                        .WithMany("RecipeFoodItems")
                        .HasForeignKey("FoodItemId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foody.Data.Models.Nutrition.Recipe", "Recipe")
                        .WithMany("RecipeFoodItems")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Foody.Data.Models.Publishing.Article", b =>
                {
                    b.HasOne("Foody.Data.Models.FoodyUser", "Author")
                        .WithMany("Articles")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("Foody.Data.Models.Publishing.Comment", b =>
                {
                    b.HasOne("Foody.Data.Models.Publishing.Article", "Article")
                        .WithMany("Comments")
                        .HasForeignKey("ArticleId");

                    b.HasOne("Foody.Data.Models.FoodyUser", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("AuthorId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Foody.Data.Models.FoodyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Foody.Data.Models.FoodyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Foody.Data.Models.FoodyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Foody.Data.Models.FoodyUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
