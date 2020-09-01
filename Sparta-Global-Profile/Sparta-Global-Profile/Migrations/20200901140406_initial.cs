using Microsoft.EntityFrameworkCore.Migrations;

namespace Sparta_Global_Profile.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserPassword",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 1,
                column: "UserTypeName",
                value: "Student");

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "UserTypeId", "UserTypeName" },
                values: new object[] { 2, "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 2);

            migrationBuilder.AlterColumn<string>(
                name: "UserPassword",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 1,
                column: "UserTypeName",
                value: "student");
        }
    }
}
