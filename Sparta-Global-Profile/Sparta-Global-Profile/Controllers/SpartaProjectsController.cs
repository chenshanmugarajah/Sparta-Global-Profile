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
        public async Task<IActionResult> Index()
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            var spartaGlobalProfileDbContext = _context.SpartaProjects.Where(sp => sp.ProfileId == profileId).Include(s => s.Profile);
            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: SpartaProjects/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(spartaProject);
        }

        // GET: SpartaProjects/Create
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId");
            return View();
        }

        // POST: SpartaProjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int SpartaProjectId, string ProjectName, string ProjectBio, int ProfileId)
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            SpartaProject spartaProject = new SpartaProject()
            {
                SpartaProjectId = SpartaProjectId,
                ProfileId = profileId,
                ProjectBio = ProjectBio,
                ProjectName = ProjectName
            };

            if (ModelState.IsValid)
            {
                _context.Add(spartaProject);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "Profile", new { id = profileId });
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", spartaProject.ProfileId);
            return View(spartaProject);
        }

        // POST: SpartaProjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int SpartaProjectId, string ProjectName, string ProjectBio, int ProfileId)
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            SpartaProject spartaProject = new SpartaProject()
            {
                SpartaProjectId = SpartaProjectId,
                ProfileId = profileId,
                ProjectBio = ProjectBio,
                ProjectName = ProjectName
            };

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
                return RedirectToAction("Index", "FullProfile");
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
            return RedirectToAction(nameof(Index));
        }

        private bool SpartaProjectExists(int id)
        {
            return _context.SpartaProjects.Any(e => e.SpartaProjectId == id);
        }
    }
}
