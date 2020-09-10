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
    public class HobbiesController : Controller
    {
        private readonly SpartaGlobalProfileDbContext _context;

        public HobbiesController(SpartaGlobalProfileDbContext context)
        {
            _context = context;
        }

        // GET: Hobbies
        public async Task<IActionResult> Index(int? id)
        {
            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == "1" && profileId != id.ToString())
            {
                return RedirectToAction("create", "hobbies", new { id = Int32.Parse(profileId) });
            }

            if (userTypeId == "2")
            {
                return RedirectToAction("index", "profile");
            }

            ViewData["Type"] = "Student";
            var spartaGlobalProfileDbContext = _context.Hobbies.Include(s => s.Profile);

            if (id != null)
            {
                spartaGlobalProfileDbContext = _context.Hobbies.Where(s => s.ProfileId == id).Include(s => s.Profile);
                ViewData["ProfileId"] = id;
                ViewData["ProfileName"] = (_context.Profiles.Where(p => p.ProfileId == id).First()).ProfileName;
            }
            else
            {
                spartaGlobalProfileDbContext = _context.Hobbies.Include(s => s.Profile);
                ViewData["Type"] = "All";
            }

            return View(await spartaGlobalProfileDbContext.ToListAsync());
        }

        // GET: Hobbies/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var hobby = await _context.Hobbies
        //        .Include(h => h.Profile)
        //        .FirstOrDefaultAsync(m => m.HobbyId == id);
        //    if (hobby == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(hobby);
        //}

        // GET: Hobbies/Create
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
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == "1" && profileId != id.ToString())
            {
                return RedirectToAction("create", "hobbies", new { id = Int32.Parse(profileId) });
            }

            if (userTypeId == "2")
            {
                return RedirectToAction("index", "profile");
            }


            return View();
        }

        // POST: Hobbies/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HobbyId,HobbyName,HobbyDescription,ProfileId")] Hobby hobby)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hobby);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Hobbies", new { id = hobby.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", hobby.ProfileId);
            return View(hobby);
        }

        // GET: Hobbies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hobby = await _context.Hobbies.FindAsync(id);
            if (hobby == null)
            {
                return NotFound();
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles.Where(p => p.ProfileId == hobby.ProfileId), "ProfileId", "ProfileName", hobby.ProfileId);
            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == hobby.ProfileId).First();

            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == "1" && profileId != id.ToString())
            {
                return RedirectToAction("index", "hobbies", new { id = Int32.Parse(profileId) });
            }

            if (userTypeId == "2")
            {
                return RedirectToAction("index", "profile");
            }

            return View(hobby);
        }

        // POST: Hobbies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HobbyId,HobbyName,HobbyDescription,ProfileId")] Hobby hobby)
        {
            if (id != hobby.HobbyId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hobby);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HobbyExists(hobby.HobbyId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Hobbies", new { id = hobby.ProfileId });
            }
            ViewData["ProfileId"] = new SelectList(_context.Profiles, "ProfileId", "ProfileId", hobby.ProfileId);
            return View(hobby);
        }

        // GET: Hobbies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hobby = await _context.Hobbies
                .Include(h => h.Profile)
                .FirstOrDefaultAsync(m => m.HobbyId == id);
            if (hobby == null)
            {
                return NotFound();
            }

            ViewData["Profile"] = _context.Profiles.Where(p => p.ProfileId == hobby.ProfileId).First();

            HttpContext context = HttpContext;
            var userId = context.Session.GetString("UserId");
            var userTypeId = context.Session.GetString("UserTypeId");
            var profileId = context.Session.GetString("ProfileId");

            if (userTypeId == null)
            {
                return RedirectToAction("index", "login");
            }

            if (userTypeId == "1" && profileId != id.ToString())
            {
                return RedirectToAction("index", "hobbies", new { id = Int32.Parse(profileId) });
            }

            if (userTypeId == "2")
            {
                return RedirectToAction("index", "profile");
            }

            return View(hobby);
        }

        // POST: Hobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hobby = await _context.Hobbies.FindAsync(id);
            _context.Hobbies.Remove(hobby);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Hobbies", new { id = hobby.ProfileId });
        }

        private bool HobbyExists(int id)
        {
            return _context.Hobbies.Any(e => e.HobbyId == id);
        }
    }
}
