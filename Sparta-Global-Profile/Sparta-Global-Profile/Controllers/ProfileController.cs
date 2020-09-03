using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Sparta_Global_Profile.Models;

namespace Sparta_Global_Profile.Controllers
{
    public class ProfileController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public ProfileController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }


        // GET: Profile
        public async Task<IActionResult> Index(string searchString,  int? pageNumber, string currentFilter)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }
            
            if(userTypeId == "1")
            {
                return RedirectToAction("Details", "Profile", new { id = profileId });
            }

            ViewData["CurrentFilter"] = searchString;

            if(searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            var profiles = from profile in _context.Profiles.Include(p => p.Course).Where(p => p.Approved == true)
                           select profile;

            if(!String.IsNullOrEmpty(searchString))
            {
                profiles = profiles.Where(p => p.Course.CourseName.Contains(searchString));
            }
            int pageSize = 3;

            return View(await PaginatedList<Profile>.CreateAsync(profiles.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        // GET: Profile/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Course)
                .Include(p => p.Status)
                .Include(p => p.User)
                .Include(p => p.Assignments)
                .Include(p => p.SpartaProjects).ThenInclude(l => l.ProjectLinks)
                .Include(p => p.Employment)
                .Include(p => p.Education).ThenInclude(e => e.Modules)
                .Include(p => p.Skills)
                .Include(p => p.Hobbies)
                .Include(p => p.Comments)
                .Include(p => p.Certifications)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // GET: Profile/Create
        public IActionResult Create()
        {
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId");
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Profile/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProfileId,UserId,StatusId,ProfileName,ProfilePicture,Summary,CourseId,Approved")] Profile profile)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", profile.CourseId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusId", profile.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", profile.UserId);
            return View(profile);
        }

        // GET: Profile/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");

            if (userId == null)
            {
                return RedirectToAction("Index", "Login");
            }

            if(userTypeId == "2")
            {
                return RedirectToAction("Index", "Profile");
            }

            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Course)
                .Include(p => p.Status)
                .Include(p => p.User)
                .Include(p => p.Assignments)
                .Include(p => p.SpartaProjects)
                .Include(p => p.Employment)
                .Include(p => p.Education).ThenInclude(e => e.Modules)
                .Include(p => p.Skills)
                .Include(p => p.Hobbies)
                .Include(p => p.Comments)
                .Include(p => p.Certifications)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", profile.CourseId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusId", profile.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", profile.UserId);
            return View(profile);
        }

        // POST: Profile/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProfileId,UserId,StatusId,ProfileName,ProfilePicture,Summary,CourseId,Approved")] Profile profile)
        {
            if (id != profile.ProfileId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.ProfileId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Courses, "CourseId", "CourseId", profile.CourseId);
            ViewData["StatusId"] = new SelectList(_context.Status, "StatusId", "StatusId", profile.StatusId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", profile.UserId);
            return View(profile);
        }

        // GET: Profile/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var profile = await _context.Profiles
                .Include(p => p.Course)
                .Include(p => p.Status)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.ProfileId == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
        }

        // POST: Profile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var profile = await _context.Profiles.FindAsync(id);
            _context.Profiles.Remove(profile);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProfileExists(int id)
        {
            return _context.Profiles.Any(e => e.ProfileId == id);
        }
    }
}
