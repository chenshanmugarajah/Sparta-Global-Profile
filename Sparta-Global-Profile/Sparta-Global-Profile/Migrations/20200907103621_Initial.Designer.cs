﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Sparta_Global_Profile.Models;

namespace Sparta_Global_Profile.Migrations
{
    [DbContext(typeof(SpartaGlobalProfileDbContext))]
    [Migration("20200907103621_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Sparta_Global_Profile.Models.Assignment", b =>
                {
                    b.Property<int>("AssignmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AssignmentId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Assignments");

                    b.HasData(
                        new
                        {
                            AssignmentId = 1,
                            CompanyName = "Home Office",
                            EndDate = new DateTime(2020, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Position = "C# Backend Developer",
                            ProfileId = 1,
                            StartDate = new DateTime(2019, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Summary = "Lots of text about how amazing this job was and what I did during it"
                        },
                        new
                        {
                            AssignmentId = 2,
                            CompanyName = "MI6",
                            EndDate = new DateTime(2020, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Position = "C# Frontend Developer",
                            ProfileId = 2,
                            StartDate = new DateTime(2019, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Summary = "Very exciting secretive job that I cannot talk about"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Certification", b =>
                {
                    b.Property<int>("CertificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CertificationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CertificationId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Certifications");

                    b.HasData(
                        new
                        {
                            CertificationId = 1,
                            CertificationName = "C# Basics course",
                            ProfileId = 1,
                            Summary = "A course that taught us about the basics of C# and how to apply them to real life scenarios"
                        },
                        new
                        {
                            CertificationId = 2,
                            CertificationName = "C# OOP course",
                            ProfileId = 1,
                            Summary = "A course that taught us about the fundamentals of object orientated programming and how it it used within C#"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CommentContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<int>("SectionIndex")
                        .HasColumnType("int");

                    b.HasKey("CommentId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AcademyExperience")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 1,
                            AcademyExperience = "all the academy pre filled stuff will be here",
                            CourseName = "C# Software Developer"
                        },
                        new
                        {
                            CourseId = 2,
                            AcademyExperience = "all the academy pre filled stuff will be here",
                            CourseName = "C# Software Development Engineer in Test (SDET)"
                        },
                        new
                        {
                            CourseId = 3,
                            AcademyExperience = "all the academy pre filled stuff will be here",
                            CourseName = "Data Engineer"
                        },
                        new
                        {
                            CourseId = 4,
                            AcademyExperience = "all the academy pre filled stuff will be here",
                            CourseName = "DevOps Consultant"
                        },
                        new
                        {
                            CourseId = 5,
                            AcademyExperience = "all the academy pre filled stuff will be here",
                            CourseName = "Java Software Developer"
                        },
                        new
                        {
                            CourseId = 6,
                            AcademyExperience = "all the academy pre filled stuff will be here",
                            CourseName = "Java Software Developer Engineer in Test (SDET)"
                        },
                        new
                        {
                            CourseId = 7,
                            AcademyExperience = "all the academy pre filled stuff will be here",
                            CourseName = "Software Developer"
                        },
                        new
                        {
                            CourseId = 8,
                            AcademyExperience = "all the academy pre filled stuff will be here",
                            CourseName = "Technology Consultant Graduate Scheme"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Education", b =>
                {
                    b.Property<int>("EducationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Establishment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Grade")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Qualification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EducationId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Educations");

                    b.HasData(
                        new
                        {
                            EducationId = 1,
                            EndDate = new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Establishment = "Royal Holloway University Of London",
                            Grade = "2.1",
                            ProfileId = 1,
                            Qualification = "BA Hons Drama & Theatre Studies",
                            StartDate = new DateTime(2015, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            EducationId = 2,
                            EndDate = new DateTime(2019, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Establishment = "Brunel University",
                            Grade = "1",
                            ProfileId = 2,
                            Qualification = "Computer Science",
                            StartDate = new DateTime(2015, 8, 21, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Employment", b =>
                {
                    b.Property<int>("EmploymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CompanyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Position")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EmploymentId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Employment");

                    b.HasData(
                        new
                        {
                            EmploymentId = 1,
                            CompanyName = "Timberland",
                            EndDate = new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Position = "Sales Assistant",
                            ProfileId = 1,
                            StartDate = new DateTime(2019, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Summary = "Was boring retail"
                        },
                        new
                        {
                            EmploymentId = 2,
                            CompanyName = "Saver",
                            EndDate = new DateTime(2020, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Position = "Supervisor",
                            ProfileId = 2,
                            StartDate = new DateTime(2019, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Summary = "Stress"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Hobby", b =>
                {
                    b.Property<int>("HobbyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HobbyDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HobbyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("HobbyId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Hobbies");

                    b.HasData(
                        new
                        {
                            HobbyId = 1,
                            HobbyDescription = "Like to play games",
                            HobbyName = "Gaming",
                            ProfileId = 1
                        },
                        new
                        {
                            HobbyId = 2,
                            HobbyDescription = "Like to keep fit",
                            HobbyName = "Gym",
                            ProfileId = 1
                        },
                        new
                        {
                            HobbyId = 3,
                            HobbyDescription = "COD Games!",
                            HobbyName = "ESporter",
                            ProfileId = 2
                        },
                        new
                        {
                            HobbyId = 4,
                            HobbyDescription = "Boulder",
                            HobbyName = "Climbing",
                            ProfileId = 2
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Module", b =>
                {
                    b.Property<int>("ModuleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CourseYear")
                        .HasColumnType("int");

                    b.Property<int>("EducationId")
                        .HasColumnType("int");

                    b.Property<string>("ModuleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ModuleId");

                    b.HasIndex("EducationId");

                    b.ToTable("Modules");

                    b.HasData(
                        new
                        {
                            ModuleId = 1,
                            CourseYear = 1,
                            EducationId = 1,
                            ModuleName = "Performance Making"
                        },
                        new
                        {
                            ModuleId = 2,
                            CourseYear = 2,
                            EducationId = 1,
                            ModuleName = "Staging The Real"
                        },
                        new
                        {
                            ModuleId = 3,
                            CourseYear = 1,
                            EducationId = 2,
                            ModuleName = "Race Relations"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Profile", b =>
                {
                    b.Property<int>("ProfileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("ProfileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ProfileId");

                    b.HasIndex("CourseId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UserId");

                    b.ToTable("Profiles");

                    b.HasData(
                        new
                        {
                            ProfileId = 1,
                            Approved = false,
                            CourseId = 1,
                            ProfileName = "Student Bruno Silva",
                            ProfilePicture = "urlpath",
                            StatusId = 1,
                            Summary = "this is a summary for my profile",
                            UserId = 1
                        },
                        new
                        {
                            ProfileId = 2,
                            Approved = false,
                            CourseId = 1,
                            ProfileName = "Student Chen Shan",
                            ProfilePicture = "This is a url to a picture",
                            StatusId = 1,
                            Summary = "this is a summary for my profile",
                            UserId = 6
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.ProjectLink", b =>
                {
                    b.Property<int>("ProjectLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LinkText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SpartaProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectLinkId");

                    b.HasIndex("SpartaProjectId");

                    b.ToTable("ProjectLinks");

                    b.HasData(
                        new
                        {
                            ProjectLinkId = 1,
                            LinkText = "Backend",
                            SpartaProjectId = 1,
                            Url = "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/labs/GameBackend"
                        },
                        new
                        {
                            ProjectLinkId = 2,
                            LinkText = "Frontend",
                            SpartaProjectId = 1,
                            Url = "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/HTML/games-web"
                        },
                        new
                        {
                            ProjectLinkId = 3,
                            LinkText = "Frontend",
                            SpartaProjectId = 2,
                            Url = "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/HTML/games-web"
                        },
                        new
                        {
                            ProjectLinkId = 4,
                            LinkText = "Frontend",
                            SpartaProjectId = 2,
                            Url = "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/HTML/games-web"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Skill", b =>
                {
                    b.Property<int>("SkillId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("SkillName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SkillId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Skills");

                    b.HasData(
                        new
                        {
                            SkillId = 1,
                            ProfileId = 1,
                            SkillName = "Agile"
                        },
                        new
                        {
                            SkillId = 2,
                            ProfileId = 1,
                            SkillName = "C#"
                        },
                        new
                        {
                            SkillId = 3,
                            ProfileId = 1,
                            SkillName = "SQL"
                        },
                        new
                        {
                            SkillId = 4,
                            ProfileId = 1,
                            SkillName = "Javascript"
                        },
                        new
                        {
                            SkillId = 5,
                            ProfileId = 2,
                            SkillName = "Agile"
                        },
                        new
                        {
                            SkillId = 6,
                            ProfileId = 2,
                            SkillName = "C#"
                        },
                        new
                        {
                            SkillId = 7,
                            ProfileId = 2,
                            SkillName = "SQL"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.SpartaProject", b =>
                {
                    b.Property<int>("SpartaProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectBio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SpartaProjectId");

                    b.HasIndex("ProfileId");

                    b.ToTable("SpartaProjects");

                    b.HasData(
                        new
                        {
                            SpartaProjectId = 1,
                            ProfileId = 1,
                            ProjectBio = "A fullstack project using ASP.NET API and React",
                            ProjectName = "Games Collector"
                        },
                        new
                        {
                            SpartaProjectId = 2,
                            ProfileId = 2,
                            ProjectBio = "A fullstack project using ASP.NET API and React",
                            ProjectName = "Blog app"
                        },
                        new
                        {
                            SpartaProjectId = 3,
                            ProfileId = 2,
                            ProjectBio = "A fullstack project using ASP.NET API and React",
                            ProjectName = "Safari Park"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Status", b =>
                {
                    b.Property<int>("StatusId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StatusName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusId");

                    b.ToTable("Status");

                    b.HasData(
                        new
                        {
                            StatusId = 1,
                            StatusName = "In Training"
                        },
                        new
                        {
                            StatusId = 2,
                            StatusName = "Preassignment"
                        },
                        new
                        {
                            StatusId = 3,
                            StatusName = "On Assignment"
                        },
                        new
                        {
                            StatusId = 4,
                            StatusName = "On Bench"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            UserEmail = "student@gmail.com",
                            UserPassword = "zbVZXNJxViq/EWHu5uGHRA==",
                            UserTypeId = 1
                        },
                        new
                        {
                            UserId = 2,
                            UserEmail = "client@gmail.com",
                            UserPassword = "zbVZXNJxViq/EWHu5uGHRA==",
                            UserTypeId = 2
                        },
                        new
                        {
                            UserId = 3,
                            UserEmail = "staff@gmail.com",
                            UserPassword = "zbVZXNJxViq/EWHu5uGHRA==",
                            UserTypeId = 3
                        },
                        new
                        {
                            UserId = 4,
                            UserEmail = "resource@gmail.com",
                            UserPassword = "zbVZXNJxViq/EWHu5uGHRA==",
                            UserTypeId = 4
                        },
                        new
                        {
                            UserId = 5,
                            UserEmail = "admin@gmail.com",
                            UserPassword = "zbVZXNJxViq/EWHu5uGHRA==",
                            UserTypeId = 5
                        },
                        new
                        {
                            UserId = 6,
                            UserEmail = "student2@gmail.com",
                            UserPassword = "zbVZXNJxViq/EWHu5uGHRA==",
                            UserTypeId = 1
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserTypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserTypes");

                    b.HasData(
                        new
                        {
                            UserTypeId = 1,
                            UserTypeName = "Student"
                        },
                        new
                        {
                            UserTypeId = 2,
                            UserTypeName = "Client"
                        },
                        new
                        {
                            UserTypeId = 3,
                            UserTypeName = "Staff"
                        },
                        new
                        {
                            UserTypeId = 4,
                            UserTypeName = "Resource Manager"
                        },
                        new
                        {
                            UserTypeId = 5,
                            UserTypeName = "Admin"
                        });
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Assignment", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Profile", "Profile")
                        .WithMany("Assignments")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Certification", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Profile", "Profile")
                        .WithMany("Certifications")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Comment", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Profile", "Profile")
                        .WithMany("Comments")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Education", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Profile", "Profile")
                        .WithMany("Education")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Employment", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Profile", "Profile")
                        .WithMany("Employment")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Hobby", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Profile", "Profile")
                        .WithMany("Hobbies")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Module", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Education", "Education")
                        .WithMany("Modules")
                        .HasForeignKey("EducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Profile", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Course", "Course")
                        .WithMany("Profiles")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sparta_Global_Profile.Models.Status", "Status")
                        .WithMany("Profiles")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Sparta_Global_Profile.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.ProjectLink", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.SpartaProject", "SpartaProject")
                        .WithMany("ProjectLinks")
                        .HasForeignKey("SpartaProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.Skill", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Profile", "Profile")
                        .WithMany("Skills")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.SpartaProject", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.Profile", "Profile")
                        .WithMany("SpartaProjects")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.User", b =>
                {
                    b.HasOne("Sparta_Global_Profile.Models.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
