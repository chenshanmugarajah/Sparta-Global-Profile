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
            var spartaGlobalProfileDbContext = _context.Employment.Include(s => s.Profile);

            if (id != null)
            {
                spartaGlobalProfileDbContext = _context.Employment.Where(s => s.ProfileId == id).Include(s => s.Profile);
                ViewData["ProfileId"] = id;
                ViewData["ProfileName"] = (_context.Profiles.Where(p => p.ProfileId == id).First()).ProfileName;
            }
            else
            {
                ViewData["Type"] = "All";
                spartaGlobalProfileDbContext = _context.Employment.Include(s => s.Profile);
            }

            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: Employments/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employment = await _context.Employment
        //        .Include(e => e.Profile)
        //        .FirstOrDefaultAsync(m => m.EmploymentId == id);
        //    if (employment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employment);
        //}

        // GET: Employments/Create
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
                return RedirectToAction("create", "employments", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View();
        }

        // POST: Employments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmploymentId,StartDate,EndDate,CompanyName,Position,Summary,ProfileId")] Employment employment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employment);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Employments", new { id = employment.ProfileId });
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

            ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == employment.ProfileId), "ProfileId", "ProfileName", employment.ProfileId);
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == employment.ProfileId).First();

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
                return RedirectToAction("index", "employments", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View(employment);
        }

        // POST: Employments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmploymentId,StartDate,EndDate,CompanyName,Position,Summary,ProfileId")] Employment employment)
        {
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
                return RedirectToAction("Index", "Employments", new { id = employment.ProfileId });
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

            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == employment.ProfileId).First();

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
                return RedirectToAction("index", "employments", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
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
            return RedirectToAction("Index", "Employments", new { id = employment.ProfileId });
        }

        private bool EmploymentExists(int id)
        {
            return _context.Employment.Any(e => e.EmploymentId == id);
        }
    }
}
