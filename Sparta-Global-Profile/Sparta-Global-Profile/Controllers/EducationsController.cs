﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sparta_Global_Profile.Models;

namespace Sparta_Global_Profile.Controllers
{
    public class EducationsController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public EducationsController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index(int? id)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetInt32("UserId");
            var userTypeId = context.Session.GetInt32("UserTypeId");
            var profileId = context.Session.GetInt32("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == 1 && profileId != id)
            {
                return RedirectToAction("create", "spartaprojects", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            ViewData["Type"] = "Student";
            var spartaGlobalProfileDbContext = _context.Educations.Include(s => s.Profile);

            if (id != null)
            {
                spartaGlobalProfileDbContext = _context.Educations.Where(s => s.ProfileId == id).Include(s => s.Profile);
                ViewData["ProfileId"] = id;
                var thisUserId = (_context.Profiles.First(p => p.ProfileId == id)).UserId;
                ViewData["ProfileName"] = (_context.Users.Where(u => u.UserId == thisUserId).First()).UserName;
            }
            else
            {
                spartaGlobalProfileDbContext = _context.Educations.Include(s => s.Profile);
                ViewData["Type"] = "All";
            }

            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: Educations/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var education = await _context.Educations
        //        .Include(e => e.Profile)
        //        .FirstOrDefaultAsync(m => m.EducationId == id);
        //    if (education == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(education);
        //}

        // GET: Educations/Create
        public IActionResult Create(int? id)
        {
            if (id != null)
            {
                ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == id), "ProfileId", "ProfileName");
                ViewData["Profile"] = id.ToString();
            }
            else
            {
                ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileName");
                ViewData["Profile"] = "0";
            }

            HttpContext context = HttpContext;
            var userId = context.Session.GetInt32("UserId");
            var userTypeId = context.Session.GetInt32("UserTypeId");
            var profileId = context.Session.GetInt32("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == 1 && profileId != id)
            {
                return RedirectToAction("create", "educations", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View();
        }

        // POST: Educations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EducationId,StartDate,EndDate,Establishment,Qualification,Grade,ProfileId")] Education education)
        {
            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Educations", new { id = education.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", education.ProfileId);
            return View(education);
        }

        // GET: Educations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = _context.Educations.Where(e => e.EducationId == id).Include(e => e.Modules).FirstOrDefault();
            if (education == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == education.ProfileId), "ProfileId", "ProfileName", education.ProfileId);
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == education.ProfileId).First();
            var profile = _context.Profiles.Where(p => p.ProfileId == education.ProfileId).First();

            HttpContext context = HttpContext;
            var userId = context.Session.GetInt32("UserId");
            var userTypeId = context.Session.GetInt32("UserTypeId");
            var profileId = context.Session.GetInt32("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == 1 && profileId != profile.ProfileId)
            {
                return RedirectToAction("index", "educations", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View(education);
        }

        // POST: Educations/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EducationId,StartDate,EndDate,Establishment,Qualification,Grade,ProfileId")] Education education)
        {
            if (id != education.EducationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(education);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationExists(education.EducationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Educations", new { id = education.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", education.ProfileId);
            return View(education);
        }

        // GET: Educations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations
                .Include(e => e.Profile)
                .FirstOrDefaultAsync(m => m.EducationId == id);
            if (education == null)
            {
                return NotFound();
            }

            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == education.ProfileId).First();
            var profile = _context.Profiles.Where(p => p.ProfileId == education.ProfileId).First();

            HttpContext context = HttpContext;
            var userId = context.Session.GetInt32("UserId");
            var userTypeId = context.Session.GetInt32("UserTypeId");
            var profileId = context.Session.GetInt32("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == 1 && profileId != profile.ProfileId)
            {
                return RedirectToAction("index", "educations", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }


            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var education = await _context.Educations.FindAsync(id);
            _context.Educations.Remove(education);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Educations", new { id = education.ProfileId });
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.EducationId == id);
        }
    }
}
