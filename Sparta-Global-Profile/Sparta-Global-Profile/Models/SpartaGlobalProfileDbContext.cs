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
            modelBuilder.Entity<Profile>()
                .HasOne(p => p.User)
                .WithOne(p => p.Profile)
                .HasForeignKey<Profile>(p => p.UserId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source = (localdb)\\mssqllocaldb;Initial Catalog = SpartaGlobalProfileDb;");
            }
        }
    }
}
