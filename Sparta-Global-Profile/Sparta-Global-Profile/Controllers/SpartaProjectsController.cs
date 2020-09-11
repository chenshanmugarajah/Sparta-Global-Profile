using System;
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
    public class SpartaProjectsController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public SpartaProjectsController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: SpartaProjects
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

            var spartaGlobalProfileDbContext = _context.SpartaProjects.Include(s => s.Profile);

            if (id != null)
            {
                spartaGlobalProfileDbContext = _context.SpartaProjects.Where(s => s.ProfileId == id).Include(s => s.Profile);
                ViewData["ProfileId"] = id;
                ViewData["ProfileName"] = (_context.Profiles.Where(p => p.ProfileId == id).First()).ProfileName;
            }
            else
            {
                spartaGlobalProfileDbContext = _context.SpartaProjects.Include(s => s.Profile);
                ViewData["Type"] = "All";
            }            

            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: SpartaProjects/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var spartaProject = await _context.SpartaProjects
        //        .Include(s => s.Profile)
        //        .FirstOrDefaultAsync(m => m.SpartaProjectId == id);
        //    if (spartaProject == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(spartaProject);
        //}

        // GET: SpartaProjects/Create
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
                return RedirectToAction("create", "spartaprojects", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View();
        }

        // POST: SpartaProjects/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SpartaProjectId,ProjectName,ProjectBio,ProfileId")] SpartaProject spartaProject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(spartaProject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "SpartaProjects", new { id = spartaProject.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", spartaProject.ProfileId);
            return View(spartaProject);
        }

        // GET: SpartaProjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spartaProject = await _context.SpartaProjects.FindAsync(id);
            if (spartaProject == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == spartaProject.ProfileId), "ProfileId", "ProfileName", spartaProject.ProfileId);
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == spartaProject.ProfileId).First();
            var profile = _context.Profiles.Where(p => p.ProfileId == spartaProject.ProfileId).First();

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
                return RedirectToAction("index", "spartaprojects", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View(spartaProject);
        }

        // POST: SpartaProjects/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SpartaProjectId,ProjectName,ProjectBio,ProfileId")] SpartaProject spartaProject)
        {
            if (id != spartaProject.SpartaProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(spartaProject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpartaProjectExists(spartaProject.SpartaProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "SpartaProjects", new { id = spartaProject.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", spartaProject.ProfileId);
            return View(spartaProject);
        }

        // GET: SpartaProjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var spartaProject = await _context.SpartaProjects
                .Include(s => s.Profile)
                .FirstOrDefaultAsync(m => m.SpartaProjectId == id);
            if (spartaProject == null)
            {
                return NotFound();
            }
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == spartaProject.ProfileId).First();
            var profile = _context.Profiles.Where(p => p.ProfileId == spartaProject.ProfileId).First();

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
                return RedirectToAction("index", "spartaprojects", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View(spartaProject);
        }

        // POST: SpartaProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var spartaProject = await _context.SpartaProjects.FindAsync(id);
            _context.SpartaProjects.Remove(spartaProject);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "SpartaProjects", new { id = spartaProject.ProfileId });

        }

        private bool SpartaProjectExists(int id)
        {
            return _context.SpartaProjects.Any(e => e.SpartaProjectId == id);
        }
    }
}
