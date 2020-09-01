using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Sparta_Global_Profile.Models
{
    public class Profile
    {
        public Profile()
        {
            Assignments = new HashSet<Assignment>();
            SpartaProjects = new HashSet<SpartaProject>();
            Employment = new HashSet<Employment>();
            Education = new HashSet<Education>();
            Skills = new HashSet<Skill>();
            Hobbies = new HashSet<Hobby>();
            Comments = new HashSet<Comment>();
            Certifications = new HashSet<Certification>();
        }
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePicture { get; set; }
        public string Summary { get; set; }
        public ICollection<Skill> Skills { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public ICollection<SpartaProject> SpartaProjects { get; set; }
        public ICollection<Employment> Employment { get; set; }
        public ICollection<Education> Education { get; set; }
        public ICollection<Certification> Certifications { get; set; }
        public ICollection<Hobby> Hobbies { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public bool Approved { get; set; }
    }
}
