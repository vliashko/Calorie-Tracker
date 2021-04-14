using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiCT.Migrations
{
    public partial class RolesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4cad2c79-df54-4ee7-ac8f-fbd52ee7567f", "37737922-bf7c-459f-b037-cd0d03b65ec9", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "493642ee-73ac-4954-8928-d8941fa14d94", "e5edbdf0-968a-446e-9ab9-c27e91b7b077", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "493642ee-73ac-4954-8928-d8941fa14d94");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4cad2c79-df54-4ee7-ac8f-fbd52ee7567f");
        }
    }
}
