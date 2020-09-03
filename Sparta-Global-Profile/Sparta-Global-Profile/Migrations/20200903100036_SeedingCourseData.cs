using Microsoft.EntityFrameworkCore.Migrations;

namespace Sparta_Global_Profile.Migrations
{
    public partial class SeedingCourseData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                column: "CourseName",
                value: "C# Software Developer");

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "AcademyExperience", "CourseName" },
                values: new object[,]
                {
                    { 2, "all the academy pre filled stuff will be here", "C# Software Development Engineer in Test (SDET)" },
                    { 3, "all the academy pre filled stuff will be here", "Data Engineer" },
                    { 4, "all the academy pre filled stuff will be here", "DevOps Consultant" },
                    { 5, "all the academy pre filled stuff will be here", "Java Software Developer" },
                    { 6, "all the academy pre filled stuff will be here", "Java Software Developer Engineer in Test (SDET)" },
                    { 7, "all the academy pre filled stuff will be here", "Software Developer" },
                    { 8, "all the academy pre filled stuff will be here", "Technology Consultant Graduate Scheme" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 8);

            migrationBuilder.UpdateData(
                table: "Courses",
                keyColumn: "CourseId",
                keyValue: 1,
                column: "CourseName",
                value: "C#");
        }
    }
}
