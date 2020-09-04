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
    public class EmploymentsController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public EmploymentsController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Employments
        public async Task<IActionResult> Index()
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            var spartaGlobalProfileDbContext = _context.Employment.Where(e => e.ProfileId == profileId).Include(e => e.Profile);
            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: Employments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employment
                .Include(e => e.Profile)
                .FirstOrDefaultAsync(m => m.EmploymentId == id);
            if (employment == null)
            {
                return NotFound();
            }

            return View(employment);
        }

        // GET: Employments/Create
        public IActionResult Create()
        {
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId");
            return View();
        }

        // POST: Employments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int EmploymentId, DateTime StartDate, DateTime EndDate, string CompanyName, string Position, string Summary)
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            Employment employment = new Employment()
            {
                ProfileId = profileId,
                EmploymentId = EmploymentId,
                StartDate = StartDate,
                EndDate = EndDate,
                CompanyName = CompanyName,
                Position = Position,
                Summary = Summary
            };

            if (ModelState.IsValid)
            {
                _context.Add(employment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Edit", "Profile", new { id = profileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", employment.ProfileId);
            return View(employment);
        }

        // GET: Employments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employment.FindAsync(id);
            if (employment == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", employment.ProfileId);
            return View(employment);
        }

        // POST: Employments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int EmploymentId, DateTime StartDate, DateTime EndDate, string CompanyName, string Position, string Summary)
        {
            HttpContext context = HttpContext;
            var profileId = Int32.Parse(context.Session.GetString("ProfileId"));

            Employment employment = new Employment()
            {
                ProfileId = profileId,
                EmploymentId = EmploymentId,
                StartDate = StartDate,
                EndDate = EndDate,
                CompanyName = CompanyName,
                Position = Position,
                Summary = Summary
            };

            if (id != employment.EmploymentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentExists(employment.EmploymentId))
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
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", employment.ProfileId);
            return View(employment);
        }

        // GET: Employments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employment = await _context.Employment
                .Include(e => e.Profile)
                .FirstOrDefaultAsync(m => m.EmploymentId == id);
            if (employment == null)
            {
                return NotFound();
            }

            return View(employment);
        }

        // POST: Employments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employment = await _context.Employment.FindAsync(id);
            _context.Employment.Remove(employment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploymentExists(int id)
        {
            return _context.Employment.Any(e => e.EmploymentId == id);
        }
    }
}
