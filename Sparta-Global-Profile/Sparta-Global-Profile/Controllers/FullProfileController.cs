﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sparta_Global_Profile.Models;
using Sparta_Global_Profile.ViewModel;

namespace Sparta_Global_Profile.Controllers
{
    public class FullProfileController : Controller
    {
        SpartaGlobalProfileDbContext _context = new SpartaGlobalProfileDbContext();
        public IActionResult Index()
        {

            HttpContext context = HttpContext;
            var userId = Convert.ToInt32(context.Session.GetString("UserId"));

            User currentUser = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();

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

        public IActionResult Edit()
        {

            HttpContext context = HttpContext;
            var userId = Convert.ToInt32(context.Session.GetString("UserId"));

            User currentUser = _context.Users.Where(u => u.UserId == userId).FirstOrDefault();

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