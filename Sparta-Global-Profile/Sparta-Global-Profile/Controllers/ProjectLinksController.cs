using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index()
        {
            var spartaGlobalProfileDbContext = _context.ProjectLinks.Include(p => p.SpartaProject);
            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: ProjectLinks/Details/5
        public async Task<IActionResult> Details(int? id)
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

            return View(projectLink);
        }

        // GET: ProjectLinks/Create
        public IActionResult Create()
        {
            ViewData["SpartaProjectId"] = new SelectList(_context.SpartaProjects, "SpartaProjectId", "SpartaProjectId");
            return View();
        }

        // POST: ProjectLinks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectLinkId,SpartaProjectId,LinkText,Url")] ProjectLink projectLink)
        {
            if (ModelState.IsValid)
            {
                _context.Add(projectLink);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
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
            ViewData["SpartaProjectId"] = new SelectList(_context.SpartaProjects, "SpartaProjectId", "SpartaProjectId", projectLink.SpartaProjectId);
            return View(projectLink);
        }

        // POST: ProjectLinks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpartaProjectId"] = new SelectList(_context.SpartaProjects, "SpartaProjectId", "SpartaProjectId", projectLink.SpartaProjectId);
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
            return RedirectToAction(nameof(Index));
        }

        private bool ProjectLinkExists(int id)
        {
            return _context.ProjectLinks.Any(e => e.ProjectLinkId == id);
        }
    }
}
