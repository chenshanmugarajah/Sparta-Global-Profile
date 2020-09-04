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
    public class EducationsController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public EducationsController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Educations
        public async Task<IActionResult> Index()
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            var spartaGlobalProfileDbContext = _context.Educations.Where(e => e.ProfileId == profileId).Include(e => e.Profile);
            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: Educations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var education = await _context.Educations
                .Include(e => e.Profile)
                .Include(e => e.Modules)
                .FirstOrDefaultAsync(m => m.EducationId == id);
            if (education == null)
            {
                return NotFound();
            }

            return View(education);
        }

        // GET: Educations/Create
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId");
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int EducationId, DateTime StartDate, DateTime EndDate, string Establishment, string Qualification, string Grade)
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            Education education = new Education()
            {
                ProfileId = profileId,
                EducationId = EducationId, 
                EndDate = EndDate,
                Establishment = Establishment,
                Grade = Grade,
                Qualification = Qualification,
                StartDate = StartDate
            };

            if (ModelState.IsValid)
            {
                _context.Add(education);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Profile", new { id = profileId });
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", education.ProfileId);
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int EducationId, DateTime StartDate, DateTime EndDate, string Establishment, string Qualification, string Grade)
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            Education education = new Education()
            {
                ProfileId = profileId,
                EducationId = EducationId,
                EndDate = EndDate,
                Establishment = Establishment,
                Grade = Grade,
                Qualification = Qualification,
                StartDate = StartDate
            };
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
                return RedirectToAction(nameof(Index));
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
            return RedirectToAction(nameof(Index));
        }

        private bool EducationExists(int id)
        {
            return _context.Educations.Any(e => e.EducationId == id);
        }
    }
}
