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
    public class ProjectLinksController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public ProjectLinksController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: ProjectLinks
        public async Task<IActionResult> Index(int? id)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            var project = _context.SpartaProjects.First(sp => sp.SpartaProjectId == id);

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == "1" && profileId != project.ProfileId.ToString())
            {
                return RedirectToAction("index", "spartaprojects", new { id = Int32.Parse(profileId) });
            }

            if (userTypeId == "2")
            {
                return RedirectToAction("index", "profile");
            }

            var spartaGlobalProfileDbContext = _context.ProjectLinks.Include(s => s.SpartaProject);

            if (id != null)
            {
                spartaGlobalProfileDbContext = _context.ProjectLinks.Where(s => s.SpartaProjectId == id).Include(s => s.SpartaProject);
            }
            else
            {
                spartaGlobalProfileDbContext = _context.ProjectLinks.Include(s => s.SpartaProject);
            }

            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: ProjectLinks/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var projectLink = await _context.ProjectLinks
        //        .Include(p => p.SpartaProject)
        //        .FirstOrDefaultAsync(m => m.ProjectLinkId == id);
        //    if (projectLink == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(projectLink);
        //}

        // GET: ProjectLinks/Create
        public IActionResult Create(int? id)
        {

            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            var project = _context.SpartaProjects.First(sp => sp.SpartaProjectId == id);
            var profile = _context.Profiles.First(profile => profile.ProfileId == project.ProfileId);

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == "1" && profileId != profile.ProfileId.ToString())
            {
                return RedirectToAction("create", "spartaprojects", new { id = Int32.Parse(profileId) });
            }

            if (userTypeId == "2")
            {
                return RedirectToAction("index", "profile");
            }

            if (id == null)
            {
                ViewData["SpartaProjectId"] = new SelectList(_context.SpartaProjects, "SpartaProjectId", "ProjectName");
            } else
            {
                ViewData["SpartaProjectId"] = new SelectList(_context.SpartaProjects.Where(pl => pl.SpartaProjectId == id), "SpartaProjectId", "ProjectName");
                ViewData["SpartaProject"] = id;
            }

            return View();
        }

        // POST: ProjectLinks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectLinkId,SpartaProjectId,LinkText,Url")] ProjectLink projectLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectLink);
                await _context.SaveChangesAsync();
                return RedirectToAction("index", "projectlinks", projectLink.SpartaProjectId);
            }
            ViewData["SpartaProjectId"] = new SelectList(_context.SpartaProjects, "SpartaProjectId", "SpartaProjectId", projectLink.SpartaProjectId);
            return View(projectLink);
        }

        // GET: ProjectLinks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectLink = await _context.ProjectLinks.FindAsync(id);
            if (projectLink == null)
            {
                return NotFound();
            }
            var project = _context.SpartaProjects.Where(sp => sp.SpartaProjectId == projectLink.SpartaProjectId).First();

            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == "1" && profileId != project.ProfileId.ToString())
            {
                return RedirectToAction("index", "projectlinks", project.ProfileId );
            }

            if (userTypeId == "2")
            {
                return RedirectToAction("index", "profile");
            }

            ViewData["SpartaProjectId"] = new SelectList(_context.SpartaProjects.Where(pl => pl.SpartaProjectId == project.SpartaProjectId), "SpartaProjectId", "ProjectName", projectLink.SpartaProjectId);
            return View(projectLink);

        }

        // POST: ProjectLinks/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProjectLinkId,SpartaProjectId,LinkText,Url")] ProjectLink projectLink)
        {
            if (id != projectLink.ProjectLinkId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectLink);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectLinkExists(projectLink.ProjectLinkId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("index", "projectlinks", projectLink.SpartaProjectId);
            }
            ViewData["SpartaProjectId"] = new SelectList(_context.SpartaProjects, "SpartaProjectId", "ProjectName", projectLink.SpartaProjectId);
            return View(projectLink);
        }

        // GET: ProjectLinks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var projectLink = await _context.ProjectLinks
                .Include(p => p.SpartaProject)
                .FirstOrDefaultAsync(m => m.ProjectLinkId == id);
            if (projectLink == null)
            {
                return NotFound();
            }

            var project = _context.SpartaProjects.Where(sp => sp.ProfileId == projectLink.SpartaProjectId).First();

            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == "1" && profileId != project.ProfileId.ToString())
            {
                return RedirectToAction("index", "projectlinks", project.ProfileId);
            }

            if (userTypeId == "2")
            {
                return RedirectToAction("index", "profile");
            }

            return View(projectLink);
        }

        // POST: ProjectLinks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var projectLink = await _context.ProjectLinks.FindAsync(id);
            _context.ProjectLinks.Remove(projectLink);
            await _context.SaveChangesAsync();
            return RedirectToAction("index", "spartaprojects", projectLink.SpartaProjectId);
        }

        private bool ProjectLinkExists(int id)
        {
            return _context.ProjectLinks.Any(e => e.ProjectLinkId == id);
        }
    }
}
