using Microsoft.EntityFrameworkCore.Migrations;

namespace Sparta_Global_Profile.Migrations
{
    public partial class addedcertifications : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Certifications",
                columns: new[] { "CertificationId", "CertificationName", "ProfileId", "Summary" },
                values: new object[] { 1, "C# Basics course", 1, "A course that taught us about the basics of C# and how to apply them to real life scenarios" });

            migrationBuilder.InsertData(
                table: "Certifications",
                columns: new[] { "CertificationId", "CertificationName", "ProfileId", "Summary" },
                values: new object[] { 2, "C# OOP course", 1, "A course that taught us about the fundamentals of object orientated programming and how it it used within C#" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Certifications",
                keyColumn: "CertificationId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Certifications",
                keyColumn: "CertificationId",
                keyValue: 2);
        }
    }
}
