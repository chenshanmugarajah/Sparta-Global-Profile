using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Sparta_Global_Profile.Models;

namespace Sparta_Global_Profile.ViewModel
{
    public class FullProfile
    {
        public int ProfileUserId { get; set; }
        public string ProfilePic { get; set; }
        public string ProfileFullName { get; set; }
        public string ProfileSummary { get; set; }
        public string ProfileStream { get; set; } // fk
        public bool ProfileApprovalStatus { get; set; }
        public int ProfileAcademyStatus { get; set; } // fk
        
        public IEnumerable<Certification> Certifications { get; set; }
        public IEnumerable<Assignment> Assignments { get; set; }
        public IEnumerable<Skill> Skills { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<Education> Educations { get; set; }
        public IEnumerable<Employment> Employments { get; set; }
        public IEnumerable<Hobby> Hobbies { get; set; }
        public IEnumerable<SpartaProject> SpartaProjects { get; set; }

    }
}
