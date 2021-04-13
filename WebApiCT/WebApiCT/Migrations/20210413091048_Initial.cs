using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCT.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Finish = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Eating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Moment = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eating", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaloriesPerMinute = table.Column<double>(type: "float", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumberOfRepetitions = table.Column<int>(type: "int", nullable: false),
                    NumberOfSets = table.Column<int>(type: "int", nullable: false),
                    RestBetweenSets = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Calories = table.Column<double>(type: "float", nullable: false),
                    Proteins = table.Column<double>(type: "float", nullable: false),
                    Fats = table.Column<double>(type: "float", nullable: false),
                    Carbohydrates = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Login = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<int>(type: "int", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ActivityExercise",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ExerciseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityExercise", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ActivityExercise_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActivityExercise_Exercises_ExerciseId",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientEating",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EatingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grams = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientEating", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientEating_Eating_EatingId",
                        column: x => x.EatingId,
                        principalTable: "Eating",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientEating_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActivityUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActivityId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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

            migrationBuilder.CreateTable(
                name: "Recipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipe_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IngredientRecipe",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Grams = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientRecipe", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientRecipe_Recipe_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Activities",
                columns: new[] { "Id", "Finish", "Name", "Start" },
                values: new object[] { new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"), new DateTime(2021, 4, 13, 8, 50, 0, 0, DateTimeKind.Unspecified), "Утреняя тренировка", new DateTime(2021, 4, 13, 8, 30, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Eating",
                columns: new[] { "Id", "Moment", "Name" },
                values: new object[,]
                {
                    { new Guid("9a91cf0c-7b9a-43ea-b87e-95e1dd30354e"), new DateTime(2021, 4, 13, 8, 55, 0, 0, DateTimeKind.Unspecified), "Завтрак" },
                    { new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"), new DateTime(2021, 4, 13, 13, 0, 0, 0, DateTimeKind.Unspecified), "Обед" }
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CaloriesPerMinute", "Description", "Name", "NumberOfRepetitions", "NumberOfSets", "RestBetweenSets" },
                values: new object[,]
                {
                    { new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e"), 5.0, "Выполняются на перекладине. Длительность 40 секунд", "Подтягивания", 10, 4, 20 },
                    { new Guid("291bf3d3-9c56-4f6c-b78e-9b100a2e7b55"), 10.0, "Из положения стоя, ноги на ширине плеч", "Приседания", 20, 3, 60 }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Calories", "Carbohydrates", "Fats", "Name", "Proteins" },
                values: new object[,]
                {
                    { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), 77.0, 16.300000000000001, 0.40000000000000002, "Картошка", 2.0 },
                    { new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3"), 98.0, 20.0, 0.40000000000000002, "Макароны", 3.6000000000000001 },
                    { new Guid("a1d8448e-b995-4783-b9d3-987c857c8c5d"), 113.0, 0.40000000000000002, 1.8999999999999999, "Куриная грудка", 23.600000000000001 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Gender", "Height", "Login", "Weight" },
                values: new object[] { new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e"), new DateTime(2002, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 0, 175, "vlyashko02", 84.200000000000003 });

            migrationBuilder.InsertData(
                table: "ActivityExercise",
                columns: new[] { "Id", "ActivityId", "ExerciseId" },
                values: new object[,]
                {
                    { new Guid("398f8ada-bf2a-491e-bce9-9cca15f45120"), new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"), new Guid("7c2a51b6-ffd3-4f82-8e21-92ca4053a37e") },
                    { new Guid("eac5d895-df3d-41aa-abcc-2915be0bb837"), new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"), new Guid("291bf3d3-9c56-4f6c-b78e-9b100a2e7b55") }
                });

            migrationBuilder.InsertData(
                table: "ActivityUser",
                columns: new[] { "Id", "ActivityId", "UserId" },
                values: new object[] { new Guid("d3f8f77c-089e-425e-b79e-eb329456463c"), new Guid("f336980a-c880-43d8-bd25-3576bcdec1f0"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") });

            migrationBuilder.InsertData(
                table: "EatingUser",
                columns: new[] { "Id", "EatingId", "UserId" },
                values: new object[,]
                {
                    { new Guid("1b3039d0-7372-47d8-bff2-5205bf580c39"), new Guid("9a91cf0c-7b9a-43ea-b87e-95e1dd30354e"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") },
                    { new Guid("10ec2edc-e38c-40b1-a83f-216c1992a457"), new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"), new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") }
                });

            migrationBuilder.InsertData(
                table: "IngredientEating",
                columns: new[] { "Id", "EatingId", "Grams", "IngredientId" },
                values: new object[,]
                {
                    { new Guid("3e687ead-a71c-4ea3-9bd4-d8596e6a5339"), new Guid("9a91cf0c-7b9a-43ea-b87e-95e1dd30354e"), 300.0, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870") },
                    { new Guid("5ef0d854-3d0f-4b99-bff0-4545a236236a"), new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"), 200.0, new Guid("3d490a70-94ce-4d15-9494-5248280c2ce3") },
                    { new Guid("f71f6aa0-a6a3-49d7-8cb9-a7e0b7c261b9"), new Guid("608ccd48-9de9-4b47-8e6c-5ee094485be8"), 100.0, new Guid("a1d8448e-b995-4783-b9d3-987c857c8c5d") }
                });

            migrationBuilder.InsertData(
                table: "Recipe",
                columns: new[] { "Id", "Description", "Name", "UserId" },
                values: new object[] { new Guid("000c0477-d0ec-472d-b65c-1b3561dac2a0"), "Также просто, как и макароны, но необычно", "Картошка с курицей", new Guid("647a9334-4fd6-4700-ba4b-5622039ab32e") });

            migrationBuilder.InsertData(
                table: "IngredientRecipe",
                columns: new[] { "Id", "Grams", "IngredientId", "RecipeId" },
                values: new object[] { new Guid("2fc02eb0-b6dd-46e7-aefc-d71f14b5ecbd"), 200.0, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), new Guid("000c0477-d0ec-472d-b65c-1b3561dac2a0") });

            migrationBuilder.InsertData(
                table: "IngredientRecipe",
                columns: new[] { "Id", "Grams", "IngredientId", "RecipeId" },
                values: new object[] { new Guid("2fc02eb0-b6dd-46e7-aefc-d71f14b5ecdb"), 100.0, new Guid("a1d8448e-b995-4783-b9d3-987c857c8c5d"), new Guid("000c0477-d0ec-472d-b65c-1b3561dac2a0") });

            migrationBuilder.CreateIndex(
                name: "IX_ActivityExercise_ActivityId",
                table: "ActivityExercise",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_ActivityExercise_ExerciseId",
                table: "ActivityExercise",
                column: "ExerciseId");

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

            migrationBuilder.CreateIndex(
                name: "IX_IngredientEating_EatingId",
                table: "IngredientEating",
                column: "EatingId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientEating_IngredientId",
                table: "IngredientEating",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_IngredientId",
                table: "IngredientRecipe",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_IngredientRecipe_RecipeId",
                table: "IngredientRecipe",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipe_UserId",
                table: "Recipe",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActivityExercise");

            migrationBuilder.DropTable(
                name: "ActivityUser");

            migrationBuilder.DropTable(
                name: "EatingUser");

            migrationBuilder.DropTable(
                name: "IngredientEating");

            migrationBuilder.DropTable(
                name: "IngredientRecipe");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Eating");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipe");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
