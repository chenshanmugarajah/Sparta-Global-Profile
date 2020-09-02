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
    [Migration("20200831152052_InitalCommit")]
    partial class InitalCommit
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

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("Qualification")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.HasKey("EducationId");

                    b.HasIndex("ProfileId");

                    b.ToTable("Educations");
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

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Profiles");
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
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.Property<string>("UserEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Sparta_Global_Profile.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("UserTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserTypes");
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
                    b.HasOne("Sparta_Global_Profile.Models.Profile", null)
                        .WithMany("Education")
                        .HasForeignKey("ProfileId");
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
                        .WithOne("Profile")
                        .HasForeignKey("Sparta_Global_Profile.Models.Profile", "UserId")
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