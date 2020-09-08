using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sparta_Global_Profile.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseName = table.Column<string>(nullable: true),
                    AcademyExperience = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                });

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    StatusId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.StatusId);
                });

            migrationBuilder.CreateTable(
                name: "UserTypes",
                columns: table => new
                {
                    UserTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserTypeName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTypes", x => x.UserTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserEmail = table.Column<string>(nullable: false),
                    UserPassword = table.Column<string>(nullable: false),
                    UserTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserTypes_UserTypeId",
                        column: x => x.UserTypeId,
                        principalTable: "UserTypes",
                        principalColumn: "UserTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    ProfileId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    StatusId = table.Column<int>(nullable: false),
                    ProfileName = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    CourseId = table.Column<int>(nullable: false),
                    Approved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.ProfileId);
                    table.ForeignKey(
                        name: "FK_Profiles_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Status",
                        principalColumn: "StatusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Assignments_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Certifications",
                columns: table => new
                {
                    CertificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CertificationName = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Certifications", x => x.CertificationId);
                    table.ForeignKey(
                        name: "FK_Certifications_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionIndex = table.Column<int>(nullable: false),
                    CommentContent = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_Comments_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Educations",
                columns: table => new
                {
                    EducationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Establishment = table.Column<string>(nullable: true),
                    Qualification = table.Column<string>(nullable: true),
                    Grade = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Educations", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_Educations_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employment",
                columns: table => new
                {
                    EmploymentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Position = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employment", x => x.EmploymentId);
                    table.ForeignKey(
                        name: "FK_Employment_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Hobbies",
                columns: table => new
                {
                    HobbyId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HobbyName = table.Column<string>(nullable: true),
                    HobbyDescription = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hobbies", x => x.HobbyId);
                    table.ForeignKey(
                        name: "FK_Hobbies_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SkillName = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                    table.ForeignKey(
                        name: "FK_Skills_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpartaProjects",
                columns: table => new
                {
                    SpartaProjectId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectBio = table.Column<string>(nullable: true),
                    ProfileId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpartaProjects", x => x.SpartaProjectId);
                    table.ForeignKey(
                        name: "FK_SpartaProjects_Profiles_ProfileId",
                        column: x => x.ProfileId,
                        principalTable: "Profiles",
                        principalColumn: "ProfileId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Modules",
                columns: table => new
                {
                    ModuleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModuleName = table.Column<string>(nullable: true),
                    CourseYear = table.Column<int>(nullable: false),
                    EducationId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modules", x => x.ModuleId);
                    table.ForeignKey(
                        name: "FK_Modules_Educations_EducationId",
                        column: x => x.EducationId,
                        principalTable: "Educations",
                        principalColumn: "EducationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectLinks",
                columns: table => new
                {
                    ProjectLinkId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpartaProjectId = table.Column<int>(nullable: false),
                    LinkText = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectLinks", x => x.ProjectLinkId);
                    table.ForeignKey(
                        name: "FK_ProjectLinks_SpartaProjects_SpartaProjectId",
                        column: x => x.SpartaProjectId,
                        principalTable: "SpartaProjects",
                        principalColumn: "SpartaProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "AcademyExperience", "CourseName" },
                values: new object[,]
                {
                    { 1, "all the academy pre filled stuff will be here", "C# Software Developer" },
                    { 2, "all the academy pre filled stuff will be here", "C# Software Development Engineer in Test (SDET)" },
                    { 3, "all the academy pre filled stuff will be here", "Data Engineer" },
                    { 4, "all the academy pre filled stuff will be here", "DevOps Consultant" },
                    { 5, "all the academy pre filled stuff will be here", "Java Software Developer" },
                    { 6, "all the academy pre filled stuff will be here", "Java Software Developer Engineer in Test (SDET)" },
                    { 7, "all the academy pre filled stuff will be here", "Software Developer" },
                    { 8, "all the academy pre filled stuff will be here", "Technology Consultant Graduate Scheme" }
                });

            migrationBuilder.InsertData(
                table: "Status",
                columns: new[] { "StatusId", "StatusName" },
                values: new object[,]
                {
                    { 4, "On Bench" },
                    { 3, "On Assignment" },
                    { 1, "In Training" },
                    { 2, "Preassignment" }
                });

            migrationBuilder.InsertData(
                table: "UserTypes",
                columns: new[] { "UserTypeId", "UserTypeName" },
                values: new object[,]
                {
                    { 4, "Resource Manager" },
                    { 1, "Student" },
                    { 2, "Client" },
                    { 3, "Staff" },
                    { 5, "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "UserEmail", "UserPassword", "UserTypeId" },
                values: new object[,]
                {
                    { 1, "student@gmail.com", "zbVZXNJxViq/EWHu5uGHRA==", 1 },
                    { 6, "student2@gmail.com", "zbVZXNJxViq/EWHu5uGHRA==", 1 },
                    { 2, "client@gmail.com", "zbVZXNJxViq/EWHu5uGHRA==", 2 },
                    { 3, "staff@gmail.com", "zbVZXNJxViq/EWHu5uGHRA==", 3 },
                    { 4, "resource@gmail.com", "zbVZXNJxViq/EWHu5uGHRA==", 4 },
                    { 5, "admin@gmail.com", "zbVZXNJxViq/EWHu5uGHRA==", 5 }
                });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "ProfileId", "Approved", "CourseId", "ProfileName", "ProfilePicture", "StatusId", "Summary", "UserId" },
                values: new object[] { 1, false, 1, "Student Bruno Silva", "urlpath", 1, "this is a summary for my profile", 1 });

            migrationBuilder.InsertData(
                table: "Profiles",
                columns: new[] { "ProfileId", "Approved", "CourseId", "ProfileName", "ProfilePicture", "StatusId", "Summary", "UserId" },
                values: new object[] { 2, false, 1, "Student Chen Shan", "This is a url to a picture", 1, "this is a summary for my profile", 6 });

            migrationBuilder.InsertData(
                table: "Assignments",
                columns: new[] { "AssignmentId", "CompanyName", "EndDate", "Position", "ProfileId", "StartDate", "Summary" },
                values: new object[,]
                {
                    { 1, "Home Office", new DateTime(2020, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Backend Developer", 1, new DateTime(2019, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Lots of text about how amazing this job was and what I did during it" },
                    { 2, "MI6", new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "C# Frontend Developer", 2, new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Very exciting secretive job that I cannot talk about" }
                });

            migrationBuilder.InsertData(
                table: "Certifications",
                columns: new[] { "CertificationId", "CertificationName", "ProfileId", "Summary" },
                values: new object[,]
                {
                    { 1, "C# Basics course", 1, "A course that taught us about the basics of C# and how to apply them to real life scenarios" },
                    { 2, "C# OOP course", 1, "A course that taught us about the fundamentals of object orientated programming and how it it used within C#" }
                });

            migrationBuilder.InsertData(
                table: "Educations",
                columns: new[] { "EducationId", "EndDate", "Establishment", "Grade", "ProfileId", "Qualification", "StartDate" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Royal Holloway University Of London", "2.1", 1, "BA Hons Drama & Theatre Studies", new DateTime(2015, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brunel University", "1", 2, "Computer Science", new DateTime(2015, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Employment",
                columns: new[] { "EmploymentId", "CompanyName", "EndDate", "Position", "ProfileId", "StartDate", "Summary" },
                values: new object[,]
                {
                    { 1, "Timberland", new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Sales Assistant", 1, new DateTime(2019, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Was boring retail" },
                    { 2, "Saver", new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Supervisor", 2, new DateTime(2019, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Stress" }
                });

            migrationBuilder.InsertData(
                table: "Hobbies",
                columns: new[] { "HobbyId", "HobbyDescription", "HobbyName", "ProfileId" },
                values: new object[,]
                {
                    { 1, "Like to play games", "Gaming", 1 },
                    { 2, "Like to keep fit", "Gym", 1 },
                    { 4, "Boulder", "Climbing", 2 },
                    { 3, "COD Games!", "ESporter", 2 }
                });

            migrationBuilder.InsertData(
                table: "Skills",
                columns: new[] { "SkillId", "ProfileId", "SkillName" },
                values: new object[,]
                {
                    { 7, 2, "SQL" },
                    { 6, 2, "C#" },
                    { 5, 2, "Agile" },
                    { 4, 1, "Javascript" },
                    { 3, 1, "SQL" },
                    { 2, 1, "C#" },
                    { 1, 1, "Agile" }
                });

            migrationBuilder.InsertData(
                table: "SpartaProjects",
                columns: new[] { "SpartaProjectId", "ProfileId", "ProjectBio", "ProjectName" },
                values: new object[,]
                {
                    { 2, 2, "A fullstack project using ASP.NET API and React", "Blog app" },
                    { 1, 1, "A fullstack project using ASP.NET API and React", "Games Collector" },
                    { 3, 2, "A fullstack project using ASP.NET API and React", "Safari Park" }
                });

            migrationBuilder.InsertData(
                table: "Modules",
                columns: new[] { "ModuleId", "CourseYear", "EducationId", "ModuleName" },
                values: new object[,]
                {
                    { 1, 1, 1, "Performance Making" },
                    { 2, 2, 1, "Staging The Real" },
                    { 3, 1, 2, "Race Relations" }
                });

            migrationBuilder.InsertData(
                table: "ProjectLinks",
                columns: new[] { "ProjectLinkId", "LinkText", "SpartaProjectId", "Url" },
                values: new object[,]
                {
                    { 1, "Backend", 1, "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/labs/GameBackend" },
                    { 2, "Frontend", 1, "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/HTML/games-web" },
                    { 3, "Frontend", 2, "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/HTML/games-web" },
                    { 4, "Frontend", 2, "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/HTML/games-web" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Assignments_ProfileId",
                table: "Assignments",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Certifications_ProfileId",
                table: "Certifications",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProfileId",
                table: "Comments",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Educations_ProfileId",
                table: "Educations",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Employment_ProfileId",
                table: "Employment",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Hobbies_ProfileId",
                table: "Hobbies",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Modules_EducationId",
                table: "Modules",
                column: "EducationId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_CourseId",
                table: "Profiles",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_StatusId",
                table: "Profiles",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectLinks_SpartaProjectId",
                table: "ProjectLinks",
                column: "SpartaProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ProfileId",
                table: "Skills",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_SpartaProjects_ProfileId",
                table: "SpartaProjects",
                column: "ProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserTypeId",
                table: "Users",
                column: "UserTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignments");

            migrationBuilder.DropTable(
                name: "Certifications");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Employment");

            migrationBuilder.DropTable(
                name: "Hobbies");

            migrationBuilder.DropTable(
                name: "Modules");

            migrationBuilder.DropTable(
                name: "ProjectLinks");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "Educations");

            migrationBuilder.DropTable(
                name: "SpartaProjects");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "UserTypes");
        }
    }
}
