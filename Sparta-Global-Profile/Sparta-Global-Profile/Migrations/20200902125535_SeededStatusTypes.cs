using Microsoft.EntityFrameworkCore.Migrations;

namespace Sparta_Global_Profile.Migrations
{
    public partial class SeededStatusTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 1,
                column: "StatusName",
                value: "In Training");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 2,
                column: "StatusName",
                value: "Preassignment");

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 3, "On Assignment" },
                    { 4, "On Bench" }
                });

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 1,
                column: "UserTypeName",
                value: "Student");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 2,
                column: "UserTypeName",
                value: "Client");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 3,
                column: "UserTypeName",
                value: "Staff");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 4,
                column: "UserTypeName",
                value: "Resource Manager");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 5,
                column: "UserTypeName",
                value: "Admin");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 4);

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 1,
                column: "StatusName",
                value: "Training");

            migrationBuilder.UpdateData(
                table: "Status",
                keyColumn: "StatusId",
                keyValue: 2,
                column: "StatusName",
                value: "Pre employment");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 1,
                column: "UserTypeName",
                value: "student");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 2,
                column: "UserTypeName",
                value: "client");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 3,
                column: "UserTypeName",
                value: "staff");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 4,
                column: "UserTypeName",
                value: "resource manager");

            migrationBuilder.UpdateData(
                table: "UserTypes",
                keyColumn: "UserTypeId",
                keyValue: 5,
                column: "UserTypeName",
                value: "admin");
        }
    }
}
