using Microsoft.EntityFrameworkCore.Migrations;

namespace CaloriesTracker.Api.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0e6d580a-4cf8-4628-8a65-0f96023a9d11");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fd66bda-7217-4706-86e5-7d083d6e0174");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "aab99d03-39bd-4750-8ee4-4eef3398d36a");

            migrationBuilder.DropColumn(
                name: "CurrentCalories",
                table: "UserProfiles");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "075b14a9-e20e-473e-bcc7-d25ce63f9524", "005d9b33-ff82-4f57-94f9-0f812be42232", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "debe5170-9207-4c73-b59d-f3658d8a98c7", "2664dce5-46d8-4f23-82cf-1cf6b5b88244", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "fd87662f-93ac-49eb-a22a-514fde73da9a", "d4a80855-4146-47e2-9f71-226ce8ecec35", "User", "USER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "075b14a9-e20e-473e-bcc7-d25ce63f9524");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "debe5170-9207-4c73-b59d-f3658d8a98c7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd87662f-93ac-49eb-a22a-514fde73da9a");

            migrationBuilder.AddColumn<float>(
                name: "CurrentCalories",
                table: "UserProfiles",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "aab99d03-39bd-4750-8ee4-4eef3398d36a", "155266f7-15e5-4333-a18e-7c44d779fb41", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3fd66bda-7217-4706-86e5-7d083d6e0174", "e9328568-3686-4c70-99c8-5d1fd16c5cbb", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "0e6d580a-4cf8-4628-8a65-0f96023a9d11", "67551579-5fdd-4796-b778-7c2e6b211754", "User", "USER" });
        }
    }
}
