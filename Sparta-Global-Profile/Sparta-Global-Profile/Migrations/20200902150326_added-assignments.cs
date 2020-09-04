using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sparta_Global_Profile.Migrations
{
    public partial class addedassignments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "AssignmentId", "CompanyName", "EndDate", "Position", "ProfileId", "StartDate", "Summary" },
                values: new object[] { 1, "Home Office", new DateTime(2020, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Backend Developer", 1, new DateTime(2019, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lots of text about how amazing this job was and what I did during it" });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "AssignmentId", "CompanyName", "EndDate", "Position", "ProfileId", "StartDate", "Summary" },
                values: new object[] { 2, "MI6", new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Frontend Developer", 2, new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Very exciting secretive job that I cannot talk about" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "AssignmentId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assignments",
                keyColumn: "AssignmentId",
                keyValue: 2);
        }
    }
}
