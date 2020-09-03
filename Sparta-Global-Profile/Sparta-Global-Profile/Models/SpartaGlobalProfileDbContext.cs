using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class SpartaGlobalProfileDbContext : DbContext
    {
        public SpartaGlobalProfileDbContext() { }

        public SpartaGlobalProfileDbContext(DbContextOptions<SpartaGlobalProfileDbContext> options) : base(options) { }

        public virtual DbSet<Assignment> Assignments { get; set; }
        public virtual DbSet<Certification> Certifications { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Education> Educations { get; set; }
        public virtual DbSet<Employment> Employment { get; set; }
        public virtual DbSet<Hobby> Hobbies { get; set; }
        public virtual DbSet<Module> Modules { get; set; }
        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<ProjectLink> ProjectLinks { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<SpartaProject> SpartaProjects { get; set; }
        public virtual DbSet<Status> Status { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserType>()
                .HasKey(userType => userType.UserTypeId);

            modelBuilder.Entity<UserType>()
                .Property(userType => userType.UserTypeName)
                .IsRequired();

            modelBuilder.Entity<UserType>().HasData
                (
                    new UserType { UserTypeId = 1, UserTypeName = "Student" },
                    new UserType { UserTypeId = 2, UserTypeName = "Client" },
                    new UserType { UserTypeId = 3, UserTypeName = "Staff" },
                    new UserType { UserTypeId = 4, UserTypeName = "Resource Manager" },
                    new UserType { UserTypeId = 5, UserTypeName = "Admin" }

                );

            modelBuilder.Entity<Status>().HasData
                (
                    new Status { StatusId = 1, StatusName = "In Training" },
                    new Status { StatusId = 2, StatusName = "Preassignment" },
                    new Status { StatusId = 3, StatusName = "On Assignment" },
                    new Status { StatusId = 4, StatusName = "On Bench" }
                );

            modelBuilder.Entity<Course>().HasData
               (
                   new Course { CourseId = 1, CourseName = "C#", AcademyExperience = "all the academy pre filled stuff will be here" }
               );

            var password = Helper.EncryptPlainTextToCipherText("123");

            modelBuilder.Entity<User>().HasData
                (
                    new User { UserId = 1, UserEmail = "student@gmail.com", UserPassword = password, UserTypeId = 1 },
                    new User { UserId = 2, UserEmail = "client@gmail.com", UserPassword = password, UserTypeId = 2 },
                    new User { UserId = 3, UserEmail = "staff@gmail.com", UserPassword = password, UserTypeId = 3},
                    new User { UserId = 4, UserEmail = "resource@gmail.com", UserPassword = password, UserTypeId = 4 },
                    new User { UserId = 5, UserEmail = "admin@gmail.com", UserPassword = password, UserTypeId = 5 }
                ); 

            modelBuilder.Entity<Profile>().HasData
                (
                    new Profile
                    {
                        ProfileId = 1,
                        UserId = 1,
                        ProfileName = "Bruno Silva",
                        ProfilePicture = "urlpath",
                        Summary = "this is a summary for my profile",
                        StatusId = 1,
                        CourseId = 1
                    }
                );

            modelBuilder.Entity<Skill>().HasData
                (
                    new Skill { SkillId = 1, SkillName = "Agile", ProfileId = 1 },
                    new Skill { SkillId = 2, SkillName = "C#", ProfileId = 1 },
                    new Skill { SkillId = 3, SkillName = "SQL", ProfileId = 1 },
                    new Skill { SkillId = 4, SkillName = "Javascript", ProfileId = 1 }
                );

            modelBuilder.Entity<Hobby>().HasData
                (
                    new Hobby { HobbyId = 1, HobbyName = "Gaming", HobbyDescription = "Like to play games", ProfileId = 1 },
                    new Hobby { HobbyId = 2, HobbyName = "Gym", HobbyDescription = "Like to keep fit", ProfileId = 1 }
                );

            modelBuilder.Entity<SpartaProject>().HasData
                (
                    new SpartaProject
                    {
                        SpartaProjectId = 1,
                        ProjectName = "Games Collector",
                        ProjectBio = "A fullstack project using ASP.NET API and React",
                        ProfileId = 1
                    }
                );

            modelBuilder.Entity<ProjectLink>().HasData
                (
                    new ProjectLink { ProjectLinkId = 1, SpartaProjectId = 1, LinkText = "Backend", Url = "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/labs/GameBackend" },
                    new ProjectLink { ProjectLinkId = 2, SpartaProjectId = 1, LinkText = "Frontend", Url = "https://github.com/Brunosil97/2020-06-c-sharp-labs/tree/master/HTML/games-web" }
                );

            modelBuilder.Entity<Education>().HasData
                (
                    new Education
                    {
                        EducationId = 1,
                        Establishment = "Royal Holloway University Of London",
                        Qualification = "BA Hons Drama & Theatre Studies",
                        Grade = "2.1",
                        StartDate = new DateTime(2015, 08, 21),
                        EndDate = new DateTime(2019, 06, 15),
                        ProfileId = 1
                    }
                );

            modelBuilder.Entity<Module>().HasData
                (
                    new Module { ModuleId = 1, EducationId = 1, CourseYear = 1, ModuleName = "Performance Making" },
                    new Module { ModuleId = 2, EducationId = 1, CourseYear = 2, ModuleName = "Staging The Real" },
                    new Module { ModuleId = 3, EducationId = 1, CourseYear = 3, ModuleName = "Race Relations" }
                );

            modelBuilder.Entity<Employment>().HasData
                (
                    new Employment
                    {
                        EmploymentId = 1,
                        ProfileId = 1,
                        CompanyName = "Timberland",
                        Position = "Sales Assistant",
                        Summary = "Was boring retail",
                        StartDate = new DateTime(2019, 09, 21),
                        EndDate = new DateTime(2020, 01, 03)
                    }
                );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\mssqllocaldb;Initial Catalog = SpartaGlobalProfileDb;");
            }
        }
    }
}
