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
    public class CertificationsController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public CertificationsController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Certifications
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
            var spartaGlobalProfileDbContext = _context.Certifications.Include(s => s.Profile);

            if (id != null)
            {
                spartaGlobalProfileDbContext = _context.Certifications.Where(s => s.ProfileId == id).Include(s => s.Profile);
                ViewData["ProfileId"] = id;
                var thisUserId = (_context.Profiles.First(p => p.ProfileId == id)).UserId;
                ViewData["ProfileName"] = (_context.Users.Where(u => u.UserId == thisUserId).First()).UserName;
            }
            else
            {
                spartaGlobalProfileDbContext = _context.Certifications.Include(s => s.Profile);
                ViewData["Type"] = "All";
            }

            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: Certifications/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var certification = await _context.Certifications
        //        .Include(c => c.Profile)
        //        .FirstOrDefaultAsync(m => m.CertificationId == id);
        //    if (certification == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(certification);
        //}

        // GET: Certifications/Create
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
                return RedirectToAction("create", "certifications", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View();
        }

        // POST: Certifications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CertificationId,CertificationName,Summary,ProfileId")] Certification certification)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certification);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Certifications", new { id = certification.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", certification.ProfileId);
            return View(certification);
        }

        // GET: Certifications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications.FindAsync(id);
            if (certification == null)
            {
                return NotFound();
            }

            ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == certification.ProfileId), "ProfileId", "ProfileName", certification.ProfileId);
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == certification.ProfileId).First();
            var profile = _context.Profiles.Where(p => p.ProfileId == certification.ProfileId).First();

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
                return RedirectToAction("index", "certifications", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View(certification);
        }

        // POST: Certifications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CertificationId,CertificationName,Summary,ProfileId")] Certification certification)
        {
            if (id != certification.CertificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certification);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificationExists(certification.CertificationId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Certifications", new { id = certification.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", certification.ProfileId);
            return View(certification);
        }

        // GET: Certifications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certification = await _context.Certifications
                .Include(c => c.Profile)
                .FirstOrDefaultAsync(m => m.CertificationId == id);
            if (certification == null)
            {
                return NotFound();
            }

            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == certification.ProfileId).First();
            var profile = _context.Profiles.Where(p => p.ProfileId == certification.ProfileId).First();

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
                return RedirectToAction("index", "certifications", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View(certification);
        }

        // POST: Certifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certification = await _context.Certifications.FindAsync(id);
            _context.Certifications.Remove(certification);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Certifications", new { id = certification.ProfileId });
        }

        private bool CertificationExists(int id)
        {
            return _context.Certifications.Any(e => e.CertificationId == id);
        }
    }
}
