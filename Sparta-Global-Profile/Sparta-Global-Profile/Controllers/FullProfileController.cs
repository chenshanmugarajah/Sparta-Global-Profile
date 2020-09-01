using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sparta_Global_Profile.Models;
using Sparta_Global_Profile.ViewModel;

namespace Sparta_Global_Profile.Controllers
{
    public class FullProfileController : Controller
    {
        public IActionResult Index()
        {
            SpartaGlobalProfileDbContext _context = new SpartaGlobalProfileDbContext();

            int TESTUSERID = 1; // session

            User currentUser = _context.Users.Where(u => u.UserId == TESTUSERID).FirstOrDefault();

            Profile profile = _context.Profiles.Where(p => p.UserId == currentUser.UserId).FirstOrDefault();

            //Profile profile = _context.Profiles.FirstOrDefault();

            FullProfile fullProfile = new FullProfile();

            fullProfile.ProfileAcademyStatus = profile.StatusId;
            fullProfile.ProfileApprovalStatus = profile.Approved;
            fullProfile.ProfileFullName = profile.ProfileName;
            fullProfile.ProfilePic = profile.ProfilePicture;

            fullProfile.Assignments = _context.Assignments.Where(a => a.ProfileId == profile.ProfileId).ToList();
            fullProfile.Certifications = _context.Certifications.Where(a => a.ProfileId == profile.ProfileId).ToList();
            fullProfile.Comments = _context.Comments.Where(a => a.ProfileId == profile.ProfileId).ToList();
            fullProfile.Educations = _context.Educations.Where(a => a.ProfileId == profile.ProfileId).ToList();
            fullProfile.Employments = _context.Employment.Where(a => a.ProfileId == profile.ProfileId).ToList();
            fullProfile.Hobbies = _context.Hobbies.Where(a => a.ProfileId == profile.ProfileId).ToList();

            // profile stream ?
            fullProfile.ProfileStream = "C# Dev";
            fullProfile.ProfileSummary = profile.Summary;

            fullProfile.Skills = _context.Skills.Where(a => a.ProfileId == profile.ProfileId).ToList();

            fullProfile.SpartaProjects = _context.SpartaProjects.Where(a => a.ProfileId == profile.ProfileId).ToList();

            return View(fullProfile);
        }
    }
}
