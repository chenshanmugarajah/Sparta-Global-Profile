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
    public class ModulesController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public ModulesController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Modules
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
            var spartaGlobalProfileDbContext = _context.Modules.Include(m => m.Education);

            if (id == null)
            {
                spartaGlobalProfileDbContext = _context.Modules.Include(m => m.Education);
                ViewData["Type"] = "All";
            } else
            {
                spartaGlobalProfileDbContext = _context.Modules.Where(m => m.EducationId == id).Include(m => m.Education);
                var education = _context.Educations.Where(e => e.EducationId == id).FirstOrDefault();
                ViewData["ProfileId"] = education.ProfileId;
                ViewData["EducationId"] = education.EducationId;
                var thisUserId = (_context.Profiles.First(p => p.ProfileId == id)).UserId;
                ViewData["ProfileName"] = (_context.Users.Where(u => u.UserId == thisUserId).First()).UserName;
            }

            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: Modules/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var module = await _context.Modules
        //        .Include(m => m.Education)
        //        .FirstOrDefaultAsync(m => m.ModuleId == id);
        //    if (module == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(module);
        //}

        // GET: Modules/Create
        public IActionResult Create(int? id)
        {
            if(id != null)
            {
                ViewData["EducationId"] = new SelectList(_context.Educations.Where(e => e.EducationId == id), "EducationId", "Establishment");
                ViewData["Education"] = id;
                ViewData["Profile"] = ((_context.Educations.Where(e => e.EducationId == id).First()).ProfileId).ToString();
            } 
            else
            {
                ViewData["EducationId"] = new SelectList(_context.Educations, "EducationId", "Establishment");
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
                return RedirectToAction("create", "modules", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View();
        }

        // POST: Modules/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ModuleId,ModuleName,CourseYear,EducationId")] Module module)
        {
            if (ModelState.IsValid)
            {
                _context.Add(module);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Modules", new { id = module.EducationId });
            }
            ViewData["EducationId"] = new SelectList(_context.Educations, "EducationId", "EducationId", module.EducationId);
            return View(module);
        }

        // GET: Modules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules.FindAsync(id);
            var education = _context.Educations.Where(e => e.EducationId == module.EducationId).FirstOrDefault();
            if (module == null)
            {
                return NotFound();
            }
            ViewData["EducationId"] = new SelectList(_context.Educations.Where(e => e.EducationId == module.EducationId), "EducationId", "Establishment", module.EducationId);
            ViewData["Education"] = education;
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == education.ProfileId).FirstOrDefault();

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
                return RedirectToAction("index", "modules", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View(module);
        }

        // POST: Modules/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ModuleId,ModuleName,CourseYear,EducationId")] Module module)
        {
            if (id != module.ModuleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.ModuleId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Modules", new { id = module.EducationId });
            }
            ViewData["EducationId"] = new SelectList(_context.Educations, "EducationId", "EducationId", module.EducationId);
            return View(module);
        }

        // GET: Modules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var module = await _context.Modules
                .Include(m => m.Education)
                .FirstOrDefaultAsync(m => m.ModuleId == id);
            if (module == null)
            {
                return NotFound();
            }

            var education = _context.Educations.Where(e => e.EducationId == module.EducationId).First();
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == education.ProfileId).First();
            ViewData["Education"] = education;

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
                return RedirectToAction("index", "modules", new { id = profileId });
            }

            if (userTypeId == 2)
            {
                return RedirectToAction("index", "profile");
            }

            return View(module);
        }

        // POST: Modules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var module = await _context.Modules.FindAsync(id);
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Educations", new { id = module.EducationId });
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(e => e.ModuleId == id);
        }
    }
}
