using Microsoft.EntityFrameworkCore.Migrations;

namespace Sparta_Global_Profile.Migrations
{
    public partial class AddNewUserTypeSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserEmail",
                value: "student@gmail.com");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "ProfileId", "UserEmail", "UserPassword", "UserTypeId" },
                values: new object[,]
                {
                    { 3, null, "staff@gmail.com", "vxFh7ubhh0Q=", 3 },
                    { 4, null, "resource@gmail.com", "vxFh7ubhh0Q=", 4 },
                    { 5, null, "admin@gmail.com", "vxFh7ubhh0Q=", 5 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 5);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "UserEmail",
                value: "bruno@gmail.com");
        }
    }
}
