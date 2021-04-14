using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCT.Migrations
{
    public partial class ChangeUserToUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientEating_Eating_EatingId",
                table: "IngredientEating");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Recipe_RecipeId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipe_Users_UserId",
                table: "Recipe");

            migrationBuilder.DropTable(
                name: "ActivityUser");

            migrationBuilder.DropTable(
                name: "EatingUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eating",
                table: "Eating");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "UserProfiles");

            migrationBuilder.RenameTable(
                name: "Recipe",
                newName: "Recipes");

            migrationBuilder.RenameTable(
                name: "Eating",
                newName: "Eatings");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Recipes",
                newName: "UserProfileId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipe_UserId",
                table: "Recipes",
                newName: "IX_Recipes_UserProfileId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eatings",
                table: "Eatings",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActivityUserProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityUserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityUserProfile_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityUserProfile_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EatingUserProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EatingUserProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EatingUserProfile_Eatings_EatingId",
                        column: x => x.EatingId,
                        principalTable: "Eatings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EatingUserProfile_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActivityUserProfile",
                columns: new[] { "Id", "ActivityId", "UserProfileId" },
                values: new object[] { new Guid("d3f8f77c-089e-425e-b79e-eb329456463c"), new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") });

            migrationBuilder.InsertData(
                table: "EatingUserProfile",
                columns: new[] { "Id", "EatingId", "UserProfileId" },
                values: new object[] { new Guid("1b3039d0-7372-47d8-bff2-5205bf580c39"), new Guid("9a91cf0c-7b9a-43ea-b87e-95e1dd30354e"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") });

            migrationBuilder.InsertData(
                table: "EatingUserProfile",
                columns: new[] { "Id", "EatingId", "UserProfileId" },
                values: new object[] { new Guid("10ec2edc-e38c-40b1-a83f-216c1992a457"), new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUserProfile_ActivityId",
                table: "ActivityUserProfile",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUserProfile_UserProfileId",
                table: "ActivityUserProfile",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_EatingUserProfile_EatingId",
                table: "EatingUserProfile",
                column: "EatingId");

            migrationBuilder.CreateIndex(
                name: "IX_EatingUserProfile_UserProfileId",
                table: "EatingUserProfile",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientEating_Eatings_EatingId",
                table: "IngredientEating",
                column: "EatingId",
                principalTable: "Eatings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Recipes_RecipeId",
                table: "IngredientRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_UserProfiles_UserProfileId",
                table: "Recipes",
                column: "UserProfileId",
                principalTable: "UserProfiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientEating_Eatings_EatingId",
                table: "IngredientEating");

            migrationBuilder.DropForeignKey(
                name: "FK_IngredientRecipe_Recipes_RecipeId",
                table: "IngredientRecipe");

            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_UserProfiles_UserProfileId",
                table: "Recipes");

            migrationBuilder.DropTable(
                name: "ActivityUserProfile");

            migrationBuilder.DropTable(
                name: "EatingUserProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfiles",
                table: "UserProfiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Eatings",
                table: "Eatings");

            migrationBuilder.RenameTable(
                name: "UserProfiles",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "Recipe");

            migrationBuilder.RenameTable(
                name: "Eatings",
                newName: "Eating");

            migrationBuilder.RenameColumn(
                name: "UserProfileId",
                table: "Recipe",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Recipes_UserProfileId",
                table: "Recipe",
                newName: "IX_Recipe_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipe",
                table: "Recipe",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Eating",
                table: "Eating",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ActivityUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityUser_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EatingUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EatingUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EatingUser_Eating_EatingId",
                        column: x => x.EatingId,
                        principalTable: "Eating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EatingUser_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActivityUser",
                columns: new[] { "Id", "ActivityId", "UserId" },
                values: new object[] { new Guid("d3f8f77c-089e-425e-b79e-eb329456463c"), new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") });

            migrationBuilder.InsertData(
                table: "EatingUser",
                columns: new[] { "Id", "EatingId", "UserId" },
                values: new object[] { new Guid("1b3039d0-7372-47d8-bff2-5205bf580c39"), new Guid("9a91cf0c-7b9a-43ea-b87e-95e1dd30354e"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") });

            migrationBuilder.InsertData(
                table: "EatingUser",
                columns: new[] { "Id", "EatingId", "UserId" },
                values: new object[] { new Guid("10ec2edc-e38c-40b1-a83f-216c1992a457"), new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUser_ActivityId",
                table: "ActivityUser",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityUser_UserId",
                table: "ActivityUser",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EatingUser_EatingId",
                table: "EatingUser",
                column: "EatingId");

            migrationBuilder.CreateIndex(
                name: "IX_EatingUser_UserId",
                table: "EatingUser",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientEating_Eating_EatingId",
                table: "IngredientEating",
                column: "EatingId",
                principalTable: "Eating",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientRecipe_Recipe_RecipeId",
                table: "IngredientRecipe",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Recipe_Users_UserId",
                table: "Recipe",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
